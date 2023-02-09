using GamesClub.Code.Core.MemoryGame.Card;
using GamesClub.Code.Data.StaticData.MemoryGame;
using GamesClub.Code.Services.StaticData;
using UnityEngine;

namespace GamesClub.Code.Services.Factories.GameFactory
{
    public class GameFactory : IGameFactory
    {
        private readonly IStaticData _staticData;

        public GameFactory(IStaticData staticData)
        {
            _staticData = staticData;
        }
        public CardView CreateCardView(MemoryCard memoryCard, Vector2 at,Transform root)
        {
            Vector2 pos = new Vector2(at.x + root.position.x, at.y + root.position.y);
            return Object.Instantiate(_staticData.Prefabs.Card, pos, Quaternion.identity, root);
        }

        public Transform CreateCardHolder() => 
            Object.Instantiate(_staticData.Prefabs.CardHolder);
    }
}