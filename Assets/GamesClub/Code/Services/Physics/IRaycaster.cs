using GamesClub.Code.Core.MemoryGame.Card;
using UnityEngine;

namespace GamesClub.Code.Services.Physics
{
    public interface IRaycaster
    {
        CardView TryGetCard(Vector2 pos);
        void CacheCamera();
    }
}