using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GamesClub.Code.Core.ClawCraneGame.Joystick;
using GamesClub.Code.Data.Enums;
using GamesClub.Code.Data.StaticData.ClawCraneGame;
using GamesClub.Code.Services.CoroutineRunner;
using GamesClub.Code.Services.SoundService;
using GamesClub.Code.Services.StaticData;
using UnityEngine;

namespace GamesClub.Code.Core.ClawCraneGame.ClawCrane
{
    public class ClawCraneMover : IClawCraneMover
    {
        public ClawCraneView View { get; private set; }
        public Coroutine MoveRoutine{ get; private set; }
        public Coroutine RopeCoroutine{ get; private set; }

        private readonly ClawCraneGameConfig _config;
        private readonly JoystickView _joystick;
        private readonly ISoundService _soundService;
        private readonly float _clawYPosition;
        private ICoroutineRunner _coroutineRunner;

        public ClawCraneMover(ClawCraneView view, IStaticData staticData, ICoroutineRunner coroutineRunner, JoystickView joystick, ISoundService soundService)
        {
            _coroutineRunner = coroutineRunner;
            View = view;
            _config = staticData.ClawCraneGameConfig;
            _clawYPosition = View.Claw.position.y;
            _joystick = joystick;
            _soundService = soundService;
        }

        public void StopSideMovement()
        {
            if(MoveRoutine == null) return;
            _coroutineRunner.StopCoroutine(MoveRoutine);
            _soundService.DisableLoopEffect();
            _joystick.Up();
        }

        public void StartMoveLeft() => MoveRoutine = _coroutineRunner.StartCoroutine(MoveLeftRoutine());

        public void StartMoveRight() => MoveRoutine = _coroutineRunner.StartCoroutine(MoveRightRoutine());

        public async void MoveClawDown(Action onComplete = null)
        {
            _soundService.EnableLoopEffect(SoundId.ClawCraneUpDown);
            StartDrawRope();
            await MoveClaw();
            StopDrawRope();
            _soundService.DisableLoopEffect();
            View.Ball.Hide();
            onComplete?.Invoke();
        }

        private void StopDrawRope() => _coroutineRunner.StopCoroutine(RopeCoroutine);

        private void StartDrawRope() => RopeCoroutine = _coroutineRunner.StartCoroutine(RopeRoutine());

        private async UniTask MoveClaw()
        {
            await View.Claw.transform.DOLocalMoveY(_config.YBallsPosition, _config.AnimationTime).AsyncWaitForCompletion();
            View.Ball.Show();
            await View.Claw.transform.DOLocalMoveY(_clawYPosition, _config.AnimationTime).AsyncWaitForCompletion();
        }

        private IEnumerator RopeRoutine()
        {
            while (true)
            {
                yield return new WaitForFixedUpdate();
                View.Rope.SetPosition(0, View.Base.position);
                View.Rope.SetPosition(1, View.Claw.position);
            }
        }

        private IEnumerator MoveLeftRoutine()
        {
            _joystick.Left();
            _soundService.EnableLoopEffect(SoundId.ClawCraneLeftRight);
            while (View.transform.position.x >= _config.LeftBoundary)
            {
                yield return new WaitForFixedUpdate();
                View.transform.position -= GetNextPosition(View.transform.position);
            }
        }
        
        private IEnumerator MoveRightRoutine()
        {
            _joystick.Right();
            _soundService.EnableLoopEffect(SoundId.ClawCraneLeftRight);
            while (View.transform.position.x <= _config.RightBoundary)
            {
                yield return new WaitForFixedUpdate();
                View.transform.position += GetNextPosition(View.transform.position);
            }
        }

        private Vector3 GetNextPosition(Vector3 pos) => 
            new (_config.MoveSpeed * Time.deltaTime, pos.y, 0);
    }
}