using System;
using UnityEngine;
using UnityEngine.UI;

namespace GamesClub.Code.Core.UI.BackButton
{
    public class BackButton : MonoBehaviour
    {
        public event Action OnBackButton;

        [SerializeField] private Button _backButton;

        private void Start() => _backButton.onClick.AddListener(OnBackButtonClicked);

        public void Show() => gameObject.SetActive(true);
        
        public void Hide() => gameObject.SetActive(false);

        public void Disable() => _backButton.interactable = false;
        
        public void Enable() => _backButton.interactable = true;
        
        private void OnBackButtonClicked() => OnBackButton?.Invoke();

        private void OnDestroy() => _backButton.onClick.RemoveListener(OnBackButtonClicked);
    }
}