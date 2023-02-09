using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GamesClub.Code.Services.InputSystem
{
    public class MobileInput : IInput
    {
        public event Action<Vector2> OnTouch; 

        private TouchControls _touchControls;

        public MobileInput()
        {
            _touchControls = new TouchControls();
            _touchControls.Disable();
        }

        public void EnableInput()
        {
            _touchControls.Enable();
            _touchControls.Touch.TouchPress.started += StartTouch;
        }

        public void DisableInput()
        {
            _touchControls.Disable();
            _touchControls.Touch.TouchPress.started -= StartTouch;
        }

        private void StartTouch(InputAction.CallbackContext ctx)
        {
            OnTouch?.Invoke(_touchControls.Touch.TouchPosition.ReadValue<Vector2>());
        }
    }
}