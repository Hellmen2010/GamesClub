using System;
using System.Collections;
using GamesClub.Code.Services.CoroutineRunner;
using UnityEngine;

namespace GamesClub.Code.Core.UI.Timer
{
    public class Timer : ITimer
    {
        public event Action OnTimeOut;

        public TimerView View;
        public float CurrentTime { get; private set; }
        public Coroutine TimerRoutine { get; private set; }
        
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly float _roundTime;
        
        public Timer(TimerView view, float roundTime, ICoroutineRunner coroutineRunner)
        {
            View = view;
            _roundTime = roundTime;
            _coroutineRunner = coroutineRunner;
            ResetTimer();
        }

        public void Start() => TimerRoutine = _coroutineRunner.StartCoroutine(Countdown());

        public void Stop()
        {
            if (TimerRoutine == null) return;
            _coroutineRunner.StopCoroutine(TimerRoutine);
        }

        public void ResetTimer()
        {
            CurrentTime = 0;
            View.SetTime(CurrentTime);
        }
        
        private IEnumerator Countdown()
        {
            while (CurrentTime < _roundTime)
            {
                CurrentTime += 0.1f;
                View.SetTime(CurrentTime);
                yield return new WaitForSeconds(0.1f);
            }
            
            TimeOut();
        }

        private void TimeOut()
        {
            Stop();
            OnTimeOut?.Invoke();
        }
    }
}