using System;
using UnityEngine;
using UnityEngine.UI;

namespace GamesClub.Code.Core.ClawCraneGame.UI
{
    public class ClawCraneControllView : MonoBehaviour
    {
        public event Action OnPressButton;
        
        public Button PressButton;
        public ClawCraneMoveButton LeftButton;
        public ClawCraneMoveButton RightButton;

        private void Start()
        {
            PressButton.onClick.AddListener(PressButtonPressed);
        }

        private void PressButtonPressed() => OnPressButton?.Invoke();

        private void OnDestroy()
        {
            PressButton.onClick.RemoveListener(PressButtonPressed);
        }

        public void DisableControl()
        {
            PressButton.interactable = false;
            LeftButton.interactable = false;
            RightButton.interactable = false;
        }
        
        public void EnableControl()
        {
            PressButton.interactable = true;
            LeftButton.interactable = true;
            RightButton.interactable = true;
        }

        public void DisableExceptLeft()
        {
            RightButton.interactable = false;
            PressButton.interactable = false;
        }

        public void DisableExceptRight()
        {
            LeftButton.interactable = false;
            PressButton.interactable = false;
        }
    }
}