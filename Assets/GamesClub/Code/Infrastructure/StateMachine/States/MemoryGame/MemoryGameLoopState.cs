using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GamesClub.Code.Core.MemoryGame.Card;
using GamesClub.Code.Core.UI.BackButton;
using GamesClub.Code.Core.UI.Timer;
using GamesClub.Code.Data.Enums;
using GamesClub.Code.Extensions;
using GamesClub.Code.Infrastructure.StateMachine.StateSwitcher;
using GamesClub.Code.Services.CardOpener;
using GamesClub.Code.Services.EntityContainer;
using GamesClub.Code.Services.InputSystem;
using GamesClub.Code.Services.Physics;
using GamesClub.Code.Services.SoundService;
using GamesClub.Code.Services.StaticData;
using UnityEngine;

namespace GamesClub.Code.Infrastructure.StateMachine.States.MemoryGame
{
    public class MemoryGameLoopState : IState
    {
        private event Action OnFirstCardClicked;
        
        private readonly IInput _input;
        private readonly IRaycaster _raycaster;
        private readonly ICardOpener _cardOpener;
        private readonly IEntityContainer _entityContainer;
        private readonly IStateSwitcher _stateSwitcher;
        private readonly IStaticData _staticData;
        private readonly ISoundService _soundService;
        private ITimer _timer;
        private TimerView _timerView;
        private Card _prevCard1;
        private Card _prevCard2;
        private int _openedCards;
        private CardDeck _deck;

        public MemoryGameLoopState(IInput input, IRaycaster raycaster, ICardOpener cardOpener, IEntityContainer entityContainer, 
            IStateSwitcher stateSwitcher, IStaticData staticData, ISoundService soundService)
        {
            _input = input;
            _raycaster = raycaster;
            _cardOpener = cardOpener;
            _entityContainer = entityContainer;
            _stateSwitcher = stateSwitcher;
            _staticData = staticData;
            _soundService = soundService;
        }
        public void Enter()
        {
            _timer = _entityContainer.GetEntity<ITimer>();
            _deck = _entityContainer.GetEntity<CardDeck>();
            Subscriptions();
            ResetLoopVariables();
            ClearPrevCardsStoring();
        }

        private void ResetLoopVariables()
        {
            _openedCards = 0;
            _raycaster.CacheCamera();
            _input.EnableInput();
            _timer.ResetTimer();
            DOTween.KillAll();
            ResetCards();
        }

        private void Subscriptions()
        {
            _input.OnTouch += OnUserTouch;
            _entityContainer.GetEntity<BackButton>().OnBackButton += MoveToChooseGame;
            OnFirstCardClicked += _timer.Start;
            _timer.OnTimeOut += MoveToResultState;
        }

        private void ResetCards()
        {
            _input.DisableInput();
            foreach (Card card in _deck.Cards)
            {
                _cardOpener.ShowCardBack(card);
                card.SetInteractable();
            }

            ShuffleCards();
            _input.EnableInput();
        }

        private void ShuffleCards()
        {
            GameplayExtensions.Shuffle(_deck.Cards);
            for (int i = 0; i < _deck.CardViews.Length; i++)
                _deck.Cards[i].View = _deck.CardViews[i];
        }

        private void MoveToChooseGame() => _stateSwitcher.SwitchTo<ChooseGameState>();

        public void Exit()
        {
            DOTween.PauseAll();
            _timer.Stop();
            _input.DisableInput();
            UnSubscriptions();
        }

        private void UnSubscriptions()
        {
            _entityContainer.GetEntity<BackButton>().OnBackButton -= MoveToChooseGame;
            _timer.OnTimeOut -= MoveToResultState;
            _input.OnTouch -= OnUserTouch;
        }

        private void OnUserTouch(Vector2 pos)
        {
            CardView view = _raycaster.TryGetCard(pos);

            if (view == null) return;
            OnFirstCardClicked?.Invoke();
            OnCardReceived(view);
            OnFirstCardClicked -= _timer.Start;
        }

        private async void OnCardReceived(CardView view)
        {
            Card card = _deck.GetCard(view.Id);
            
            if (_prevCard1 == card) return;
            
            _input.DisableInput();
            
            StoreCard(card);
            
            _soundService.PlayEffectSound(SoundId.CardRotation);
            await _cardOpener.ShowCardFront(card);

            await BothCardsOpen();

            if (IsRoundEnd()) 
                MoveToResultState();

            _input.EnableInput();
        }

        private bool IsRoundEnd() => _openedCards >= _staticData.MemoryGameConfig.Pairs.Length * 2;

        private async UniTask BothCardsOpen()
        {
            if (_prevCard1 != null && _prevCard2 != null)
            {
                if (_cardOpener.CheckIsPair(_prevCard1, _prevCard2))
                {
                    _soundService.PlayEffectSound(SoundId.RightPair);
                    _openedCards += 2;
                    _prevCard1.SetNonInteractable();
                    _prevCard2.SetNonInteractable();
                }
                else
                {
                    _soundService.PlayEffectSound(SoundId.WrongPair);
                    _cardOpener.ShowCardBack(_prevCard1);
                    await _cardOpener.ShowCardBack(_prevCard2);
                }

                ClearPrevCardsStoring();
            }
        }

        private void StoreCard(Card card)
        {
            if (_prevCard1 == null) _prevCard1 = card;
            else _prevCard2 = card;
        }

        private void ClearPrevCardsStoring() => 
            _prevCard1 = _prevCard2 = null;

        private void MoveToResultState() => _stateSwitcher.SwitchTo<MemoryGameResultsState>();
    }
}