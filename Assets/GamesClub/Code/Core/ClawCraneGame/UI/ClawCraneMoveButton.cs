using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GamesClub.Code.Core.ClawCraneGame.UI
{
    public class ClawCraneMoveButton : Button
    {
        public event Action OnPressed;
        public event Action OnReleased;
        
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            if(!interactable) return;
            OnPressed?.Invoke();
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            if(!interactable) return;
            OnReleased?.Invoke();
        }
    }
}