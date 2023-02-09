using GamesClub.Code.Core.MemoryGame.Card;
using GamesClub.Code.Core.UI.BackButton;
using GamesClub.Code.Core.UI.Score;
using GamesClub.Code.Data.StaticData.MemoryGame;
using GamesClub.Code.Infrastructure.StateMachine.StateSwitcher;
using GamesClub.Code.Services.EntityContainer;
using GamesClub.Code.Services.Factories.GameFactory;
using GamesClub.Code.Services.Factories.UIFactory;
using GamesClub.Code.Services.SceneLoader;
using GamesClub.Code.Services.StaticData;
using UnityEngine;

namespace GamesClub.Code.Infrastructure.StateMachine.States.MemoryGame
{
    public class MemoryGameCreationState : IState
    {
        private const string MemoryGameScene = "MemoryGame";
        private readonly IStateSwitcher _stateSwitcher;
        private readonly ISceneLoader _sceneLoader;
        private readonly IEntityContainer _entityContainer;
        private readonly IStaticData _staticData;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        private CardDeck _deck;

        public MemoryGameCreationState(IStateSwitcher stateSwitcher, ISceneLoader sceneLoader, 
            IEntityContainer entityContainer, IStaticData staticData, IGameFactory gameFactory, IUIFactory uiFactory)
        {
            _stateSwitcher = stateSwitcher;
            _sceneLoader = sceneLoader;
            _entityContainer = entityContainer;
            _staticData = staticData;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
        }

        public void Enter()
        {
            _sceneLoader.LoadScene(MemoryGameScene, CreateGame);
            SetupTopPanel();
        }

        private void CreateUI()
        {
            _uiFactory.CreateTimer();
            Transform root = _uiFactory.CreateRootCanvas().transform;
            _uiFactory.CreateResultPopUp(root);
        }
        
        public void Exit()
        {
            _entityContainer.GetEntity<BackButton>().OnBackButton -= MoveToChooseGame;
        }

        private void MoveToGameLoopState() => 
            _stateSwitcher.SwitchTo<MemoryGameLoopState>();

        private void SetupTopPanel()
        {
            _entityContainer.GetEntity<BackButton>().Show();
            _entityContainer.GetEntity<ScoreView>().Show();
            _entityContainer.GetEntity<BackButton>().OnBackButton += MoveToChooseGame;
        }

        private void MoveToChooseGame() => _stateSwitcher.SwitchTo<ChooseGameState>();

        private void CreateGame()
        {
            CreateUI();
            CreateCards();
            MoveToGameLoopState();
        }

        private void CreateCards()
        {
            Transform holder = _gameFactory.CreateCardHolder();

            _deck = new CardDeck(
                new Card[_staticData.CardsCoord.Length],
                new CardView[_staticData.CardsCoord.Length]);
            _entityContainer.RegisterEntity(_deck);

            MemoryGameConfig config = _staticData.MemoryGameConfig;
            Vector2[] coords = _staticData.CardsCoord;

            for (int i = 0; i < coords.Length; i += 2)
            {
                int oneFromPair = i / 2;
                CreateCard(config, oneFromPair, coords, i, holder);
                CreateCard(config, oneFromPair, coords, i+1, holder);
            }
        }

        private void CreateCard(MemoryGameConfig config, int oneFromPair, Vector2[] coords, int i, Transform holder)
        {
            CardView view = _gameFactory.CreateCardView(config.Pairs[oneFromPair], coords[i], holder);
            _deck.CardViews[i] = view;
            view.Construct(i);
            _deck.Cards[i] = new Card(view, config.Pairs[oneFromPair]);
        }
    }
}