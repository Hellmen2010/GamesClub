using System;
using GamesClub.Code.Data.Enums;
using UnityEngine;

namespace GamesClub.Code.Data.StaticData.MemoryGame
{
    [Serializable]
    public class MemoryCard
    {
        public Sprite CardFront;
        public MemoryCardType CardType;
    }
}