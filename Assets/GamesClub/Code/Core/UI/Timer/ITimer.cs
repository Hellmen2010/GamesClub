using System;

namespace GamesClub.Code.Core.UI.Timer
{
    public interface ITimer
    {
        float CurrentTime { get; }
        event Action OnTimeOut;
        void ResetTimer();
        void Start();
        void Stop();
    }
}