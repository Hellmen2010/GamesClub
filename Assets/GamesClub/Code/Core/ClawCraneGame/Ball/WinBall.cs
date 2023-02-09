using UnityEngine;

namespace GamesClub.Code.Core.ClawCraneGame.Ball
{
    public class WinBall : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _view;

        public void SetView(Sprite view) => _view.sprite = view;

        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);
    }
}