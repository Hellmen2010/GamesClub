using UnityEngine;
using UnityEngine.UI;

namespace GamesClub.Code.Core.SceneIni
{
    public class Background : MonoBehaviour
    {
        [SerializeField] private Sprite _iPhoneBackground;
        [SerializeField] private Sprite _iPadBackground;
        [SerializeField] private Image _backgroundImage;

        private const int iPadHeight = 1200;

        private void Start() => SetQuality();

        private void SetQuality() =>
            _backgroundImage.sprite = Screen.width >= iPadHeight ? _iPadBackground : _iPhoneBackground;

    }
}