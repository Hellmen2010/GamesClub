using GamesClub.Code.Core.ClawCraneGame.ClawCrane;
using GamesClub.Code.Core.ClawCraneGame.UI;
using GamesClub.Code.Core.UI.BackButton;
using GamesClub.Code.Data.StaticData.ClawCraneGame;
using GamesClub.Code.Infrastructure.StateMachine.StateSwitcher;
using GamesClub.Code.Services.CoroutineRunner;
using GamesClub.Code.Services.EntityContainer;
using GamesClub.Code.Services.StaticData;
using UnityEngine;

namespace GamesClub.Code.Infrastructure.StateMachine.States.ClawCrane
{
    public class ClawCraneGameLoopState : IState
    {
        private readonly IEntityContainer _entityContainer;
        private readonly IStaticData _staticData;
        private readonly IStateSwitcher _stateSwitcher;
        private readonly ICoroutineRunner _coroutineRunner;
        private BallData _winBallData;
        private ClawCraneControllView _clawCraneControll;
        private IClawCraneMover _clawCraneMover;
        private BackButton _backButton;

        public ClawCraneGameLoopState(IEntityContainer entityContainer, IStaticData staticData, IStateSwitcher stateSwitcher, ICoroutineRunner coroutineRunner)
        {
            _entityContainer = entityContainer;
            _staticData = staticData;
            _stateSwitcher = stateSwitcher;
            _coroutineRunner = coroutineRunner;
        }
        public void Enter()
        {
            _backButton = _entityContainer.GetEntity<BackButton>();
            _clawCraneControll = _entityContainer.GetEntity<ClawCraneControllView>();
            _clawCraneMover = _entityContainer.GetEntity<IClawCraneMover>();
            _clawCraneControll.EnableControl();
            Subscriptions();
            SetupWinData();
        }

        private void MoveToChooseGame()
        {
            _stateSwitcher.SwitchTo<ChooseGameState>();
        }

        private void SetupWinData()
        {
            int winBallID = Random.Range(0, _staticData.ClawCraneGameConfig.Wins.Length);
            _winBallData = _staticData.ClawCraneGameConfig.Wins[winBallID];
            _clawCraneMover.View.Ball.SetView(_winBallData.Ball);
            _entityContainer.GetEntity<ClawCraneWinPopUp>().SetView(_winBallData);
        }

        public void Exit()
        {
            _backButton.Enable();
            UnSubscriptions();
        }

        private void Subscriptions()
        {
            _clawCraneControll.LeftButton.OnPressed += StartMoveLeft;
            _clawCraneControll.LeftButton.OnReleased += StopSideMove;
            _clawCraneControll.RightButton.OnPressed += StartMoveRight;
            _clawCraneControll.RightButton.OnReleased += StopSideMove;
            _backButton.OnBackButton += MoveToChooseGame;
            _clawCraneControll.OnPressButton += GrabBall;
        }

        private void StartMoveLeft()
        {
            _clawCraneControll.DisableExceptLeft();
            _clawCraneMover.StartMoveLeft();
            _backButton.Disable();
        }
        
        private void StartMoveRight()
        {
            _clawCraneControll.DisableExceptRight();
            _clawCraneMover.StartMoveRight();
            _backButton.Disable();
        }
        
        private void StopSideMove()
        {
            _clawCraneControll.EnableControl();
            _clawCraneMover.StopSideMovement();
            _backButton.Enable();
        }

        private void MoveToResultState() => 
            _stateSwitcher.SwitchTo<ClawCraneResultsState>(_winBallData.Score);

        private void GrabBall()
        {
            _backButton.Disable();
            _clawCraneControll.DisableControl();
            _clawCraneMover.MoveClawDown(MoveToResultState);
        }

        private void UnSubscriptions()
        {
            _clawCraneControll.LeftButton.OnPressed -= StartMoveLeft;
            _clawCraneControll.LeftButton.OnReleased -= StopSideMove;
            _clawCraneControll.RightButton.OnPressed -= StartMoveRight;
            _clawCraneControll.RightButton.OnReleased -= StopSideMove;
            _backButton.OnBackButton -= MoveToChooseGame;
            _clawCraneControll.OnPressButton -= GrabBall;
        }
    }
}