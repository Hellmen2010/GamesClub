using UnityEngine;

namespace GamesClub.Code.Data.StaticData.MemoryGame
{
    [CreateAssetMenu(fileName = "MemoryGameConfig", menuName = "Static Data/MemoryGameConfig")]
    public class MemoryGameConfig : ScriptableObject
    {
        public Sprite CardBack;
        public MemoryCard[] Pairs;

        public int CardsInRow;
        public float LengthBtwCardsHorizontal;
        public float LengthBtwCardsVertical;

        public float[] ScoreArray;
        public float RoundTime;
    }
}