using GamesClub.Code.Data.Enums;
using GamesClub.Code.Data.StaticData.MemoryGame;
using UnityEngine;

namespace GamesClub.Code.Core.MemoryGame.Card
{
    public class Card
    {
        public bool IsFront { get; set; }
        public MemoryCardType CardType { get; private set; }
        public CardView View { get; set; }
        public Sprite CardFront { get; private set; }

        public Card(CardView view, MemoryCard card)
        {
            View = view;
            CardFront = card.CardFront;
            CardType = card.CardType;
        }

        public void SetNonInteractable() => View.gameObject.layer = default;

        public void SetInteractable()
        {
            View.gameObject.layer = 8;
        }
    }
}