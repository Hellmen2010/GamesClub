using System;
using UnityEngine;

namespace GamesClub.Code.Services.InputSystem
{
    public interface IInput
    {
        event Action<Vector2> OnTouch;
        void EnableInput();
        void DisableInput();
    }
}