using GamesClub.Code.Data.StaticData.ClawCraneGame;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GamesClub.Code.Core.ClawCraneGame.UI
{
    public class ClawCraneWinPopUp : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Image _ball;
        [SerializeField] private Image _glow;
        [SerializeField] private Image _prize;
        [SerializeField] private TMP_Text _winScore;

        public Button CloseButton => _closeButton;
        
        public void SetView(BallData data)
        {
            _ball.sprite = data.Ball;
            _glow.sprite = data.Glow;
            _prize.sprite = data.Prize;
            _winScore.text = data.Score.ToString();
        }

        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);
    }
}