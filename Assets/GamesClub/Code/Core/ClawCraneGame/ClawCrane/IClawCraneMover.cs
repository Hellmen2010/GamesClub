using System;
using UnityEngine;

namespace GamesClub.Code.Core.ClawCraneGame.ClawCrane
{
    public interface IClawCraneMover
    {
        ClawCraneView View { get; }
        Coroutine MoveRoutine { get; }
        Coroutine RopeCoroutine { get; }
        void StopSideMovement();
        void StartMoveLeft();
        void StartMoveRight();
        void MoveClawDown(Action OnComplete = null);
    }
}