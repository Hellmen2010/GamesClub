using GamesClub.Code.Core.MemoryGame.Card;
using GamesClub.Code.Data.StaticData.MemoryGame;
using UnityEngine;

namespace GamesClub.Code.Services.Factories.GameFactory
{
    public interface IGameFactory
    {
        CardView CreateCardView(MemoryCard memoryCard, Vector2 at, Transform transform);
        Transform CreateCardHolder();
    }
}