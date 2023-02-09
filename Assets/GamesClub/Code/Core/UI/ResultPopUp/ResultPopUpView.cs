using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GamesClub.Code.Core.UI.ResultPopUp
{
    public class ResultPopUpView : MonoBehaviour
    {
        public event Action OnBackButton;
        public event Action OnRestartButton;

        [SerializeField] private GameObject _popUp;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _timeText;

        private void Start()
        {
            _backButton.onClick.AddListener(BackButtonPressed);
            _restartButton.onClick.AddListener(Restart);
        }

        public void Hide() => _popUp.SetActive(false);
        
        public void Show() => _popUp.SetActive(true);

        public void SetResultText(float score) => _scoreText.text = "Score: " + score.ToString("F1");

        public void SetTimeText(float time) => _timeText.text = "Time: " +  time.ToString("F1");

        private void BackButtonPressed() => OnBackButton?.Invoke();
        private void Restart() => OnRestartButton?.Invoke();

        private void OnDestroy()
        {
            _backButton.onClick.RemoveListener(BackButtonPressed);
            _restartButton.onClick.RemoveListener(Restart);
        }
    }
}