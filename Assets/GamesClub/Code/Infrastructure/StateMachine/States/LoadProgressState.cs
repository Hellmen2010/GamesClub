using GamesClub.Code.Data.Progress;
using GamesClub.Code.Infrastructure.StateMachine.StateSwitcher;
using GamesClub.Code.Services.EntityContainer;
using GamesClub.Code.Services.PersistentProgress;
using GamesClub.Code.Services.SaveLoad;
using GamesClub.Code.Services.SoundService;
using GamesClub.Code.Services.StaticData;

namespace GamesClub.Code.Infrastructure.StateMachine.States
{
    public class LoadProgressState : IState
    {
        private readonly IStateSwitcher _stateSwitcher;
        private readonly IPersistentProgress _playerProgress;
        private readonly ISaveLoad _saveLoadService;
        private readonly IEntityContainer _entityContainer;
        private readonly IStaticData _staticData;
        private readonly ISoundService _soundService;

        public LoadProgressState(IStateSwitcher stateSwitcher, IPersistentProgress playerProgress,
            ISaveLoad saveLoadService, IEntityContainer entityContainer, IStaticData staticData, ISoundService soundService)
        {
            _saveLoadService = saveLoadService;
            _entityContainer = entityContainer;
            _staticData = staticData;
            _soundService = soundService;
            _playerProgress = playerProgress;
            _stateSwitcher = stateSwitcher;
        }
        
        public void Enter()
        {
            LoadProgressOrInitNew();
            _soundService.Construct(_staticData.Sounds, _playerProgress.Progress.Settings);
            _stateSwitcher.SwitchTo<LoadPersistentEntityState>();
        }

        public void Exit()
        {
        }
        
        private void LoadProgressOrInitNew() => 
            _playerProgress.Progress = _saveLoadService.LoadProgress() ?? new PlayerProgress();
    }
}