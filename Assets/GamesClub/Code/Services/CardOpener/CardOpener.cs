using Cysharp.Threading.Tasks;
using DG.Tweening;
using GamesClub.Code.Core.MemoryGame.Card;
using GamesClub.Code.Data.Enums;
using GamesClub.Code.Services.SoundService;
using GamesClub.Code.Services.StaticData;
using UnityEngine;

namespace GamesClub.Code.Services.CardOpener
{
    public class CardOpener : ICardOpener
    {
        private float _halfTurnTime = 0.25f;
        private Sprite _cardBack;

        public CardOpener(IStaticData staticData)
        {
            _cardBack = staticData.MemoryGameConfig.CardBack;
        }

        public bool CheckIsPair(Card prevCard1, Card prevCard2) => 
            prevCard1.CardType == prevCard2.CardType;

        public async UniTask ShowCardFront(Card card)
        {
            await card.View.transform.DOLocalRotate(new Vector3(0, 90f, 0), _halfTurnTime).AsyncWaitForCompletion();
            if(card.View == null) return;
            card.View.SetNewSprite(card.CardFront);
            await card.View.transform.DOLocalRotate(new Vector3(0, 180f, 0), _halfTurnTime).AsyncWaitForCompletion();
            card.IsFront = true;
        }
        
        public async UniTask ShowCardBack(Card card)
        {
            await card.View.transform.DOLocalRotate(new Vector3(0, 270f, 0), _halfTurnTime).AsyncWaitForCompletion();
            if(card.View == null) return;
            card.View.SetNewSprite(_cardBack);
            await card.View.transform.DOLocalRotate(new Vector3(0, 0, 0), _halfTurnTime).AsyncWaitForCompletion();
            card.IsFront = false;
        }
    }
}