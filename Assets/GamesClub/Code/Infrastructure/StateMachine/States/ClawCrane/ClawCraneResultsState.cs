using GamesClub.Code.Core.ClawCraneGame.UI;
using GamesClub.Code.Core.UI.BackButton;
using GamesClub.Code.Core.UI.Score;
using GamesClub.Code.Data.Enums;
using GamesClub.Code.Infrastructure.StateMachine.StateSwitcher;
using GamesClub.Code.Services.EntityContainer;
using GamesClub.Code.Services.PersistentProgress;
using GamesClub.Code.Services.SaveLoad;
using GamesClub.Code.Services.ScoreService;
using GamesClub.Code.Services.SoundService;

namespace GamesClub.Code.Infrastructure.StateMachine.States.ClawCrane
{
    public class ClawCraneResultsState : IPayloadedState
    {
        private readonly IEntityContainer _entityContainer;
        private readonly IStateSwitcher _stateSwitcher;
        private readonly IScoreService _scoreService;
        private readonly ISoundService _soundService;
        private ClawCraneWinPopUp _clawCraneWinPopUp;

        public ClawCraneResultsState(IEntityContainer entityContainer, IStateSwitcher stateSwitcher, IScoreService scoreService, ISoundService soundService)
        {
            _entityContainer = entityContainer;
            _stateSwitcher = stateSwitcher;
            _scoreService = scoreService;
            _soundService = soundService;
        }
        public void Enter(object ballScore)
        {
            _clawCraneWinPopUp = _entityContainer.GetEntity<ClawCraneWinPopUp>();
            _clawCraneWinPopUp.CloseButton.onClick.AddListener(MoveToGameLoop);
            _soundService.PlayEffectSound(SoundId.WinSound);
            _clawCraneWinPopUp.Show();
            _scoreService.ChangeScore((float)ballScore);
            _entityContainer.GetEntity<ScoreView>().SetScoreText(_scoreService.Score);
            _entityContainer.GetEntity<BackButton>().OnBackButton += MoveToChooseGame;
        }

        public void Exit()
        {
            _entityContainer.GetEntity<BackButton>().OnBackButton -= MoveToChooseGame;
            _clawCraneWinPopUp.CloseButton.onClick.RemoveListener(MoveToGameLoop);
            _clawCraneWinPopUp.Hide();
        }

        private void MoveToGameLoop() => _stateSwitcher.SwitchTo<ClawCraneGameLoopState>();
        
        private void MoveToChooseGame() => _stateSwitcher.SwitchTo<ChooseGameState>();
    }
}