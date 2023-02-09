using GamesClub.Code.Core.MemoryGame.Card;
using UnityEngine;

namespace GamesClub.Code.Services.Physics
{
    public class Raycaster : IRaycaster
    {
        private const int CardLayerMask = 1 << 8;
        private const float _rayLength = 10f;
        private Camera _camera;

        public void CacheCamera() => _camera = Camera.main;

        public CardView TryGetCard(Vector2 pos)
        {
            Ray ray = Camera.main.ScreenPointToRay(pos);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, _rayLength, CardLayerMask);

            if (hit.collider == null) return null;

            hit.collider.TryGetComponent(out CardView view);
            return view;
        }
    }
}