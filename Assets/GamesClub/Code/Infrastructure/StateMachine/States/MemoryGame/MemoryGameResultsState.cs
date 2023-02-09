using System.Linq;
using GamesClub.Code.Core.UI.ResultPopUp;
using GamesClub.Code.Core.UI.Score;
using GamesClub.Code.Core.UI.Timer;
using GamesClub.Code.Data.Enums;
using GamesClub.Code.Infrastructure.StateMachine.StateSwitcher;
using GamesClub.Code.Services.EntityContainer;
using GamesClub.Code.Services.ScoreService;
using GamesClub.Code.Services.SoundService;
using GamesClub.Code.Services.StaticData;

namespace GamesClub.Code.Infrastructure.StateMachine.States.MemoryGame
{
    public class MemoryGameResultsState : IState
    {
        private readonly IEntityContainer _entityContainer;
        private readonly IStateSwitcher _stateSwitcher;
        private readonly IStaticData _staticData;
        private readonly IScoreService _scoreService;
        private readonly ISoundService _soundService;

        public MemoryGameResultsState(IEntityContainer entityContainer, IStateSwitcher stateSwitcher, 
            IStaticData staticData, IScoreService scoreService, ISoundService soundService)
        {
            _entityContainer = entityContainer;
            _stateSwitcher = stateSwitcher;
            _staticData = staticData;
            _scoreService = scoreService;
            _soundService = soundService;
        }
        public void Enter()
        {
            float currentTime = _entityContainer.GetEntity<ITimer>().CurrentTime;
            float currentScore = SetResults(currentTime);
            _scoreService.ChangeScore(currentScore);
            _entityContainer.GetEntity<ScoreView>().SetScoreText(_scoreService.Score);
            Subscribtions();
            _entityContainer.GetEntity<ResultPopUpView>().Show();
            _soundService.PlayEffectSound(SoundId.WinSound);
        }

        private void Subscribtions()
        {
            _entityContainer.GetEntity<ResultPopUpView>().OnBackButton += MoveToChooseGame;
            _entityContainer.GetEntity<ResultPopUpView>().OnRestartButton += RestartGame;
        }

        private float SetResults(float currentTime)
        {
            float[] scoreArray = _staticData.MemoryGameConfig.ScoreArray;
            float scorePerDeltaTime = _staticData.MemoryGameConfig.RoundTime / scoreArray.Length;

            float currenScore = scoreArray.Where((t, i) => currentTime < (i + 1) * scorePerDeltaTime).FirstOrDefault();

            _entityContainer.GetEntity<ResultPopUpView>().SetTimeText(currentTime);
            _entityContainer.GetEntity<ResultPopUpView>().SetResultText(currenScore);
            return currenScore;
        }

        public void Exit()
        {
            UnSubscriptions();
        }

        private void UnSubscriptions()
        {
            _entityContainer.GetEntity<ResultPopUpView>().OnBackButton -= MoveToChooseGame;
            _entityContainer.GetEntity<ResultPopUpView>().OnRestartButton -= RestartGame;
            _entityContainer.GetEntity<ResultPopUpView>().Hide();
        }

        private void MoveToChooseGame() => _stateSwitcher.SwitchTo<ChooseGameState>();
        private void RestartGame() => _stateSwitcher.SwitchTo<MemoryGameLoopState>();
    }
}