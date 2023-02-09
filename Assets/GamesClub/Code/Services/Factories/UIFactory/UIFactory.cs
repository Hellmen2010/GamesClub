using GamesClub.Code.Core.UI.BackButton;
using GamesClub.Code.Core.UI.GameVariant;
using GamesClub.Code.Core.UI.ResultPopUp;
using GamesClub.Code.Core.UI.Score;
using GamesClub.Code.Core.UI.Settings;
using GamesClub.Code.Core.UI.Timer;
using GamesClub.Code.Services.CoroutineRunner;
using GamesClub.Code.Services.EntityContainer;
using GamesClub.Code.Services.PersistentProgress;
using GamesClub.Code.Services.SaveLoad;
using GamesClub.Code.Services.SoundService;
using GamesClub.Code.Services.StaticData;
using UnityEngine;
using UnityEngine.UI;

namespace GamesClub.Code.Services.Factories.UIFactory
{
    public class UIFactory : IUIFactory
    {
        private readonly IStaticData _staticData;
        private readonly IEntityContainer _entityContainer;
        private readonly IPersistentProgress _persistentProgress;
        private readonly ISaveLoad _saveLoad;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly ISoundService _soundService;

        public UIFactory(IStaticData staticData, IEntityContainer entityContainer, 
            IPersistentProgress persistentProgress, ISaveLoad saveLoad, ICoroutineRunner coroutineRunner, ISoundService soundService)
        {
            _staticData = staticData;
            _entityContainer = entityContainer;
            _persistentProgress = persistentProgress;
            _saveLoad = saveLoad;
            _coroutineRunner = coroutineRunner;
            _soundService = soundService;
        }
        
        public GameObject CreateRootCanvas() => 
            Object.Instantiate(_staticData.Prefabs.RootCanvasPrefab);
        
        public SettingsView CreateSettings(Transform parent)
        {
            Button settingsButton = CreateSettingsButton(parent);

            SettingsView settingsView = Object.Instantiate(_staticData.Prefabs.SettingsPrefab, parent);
            settingsView.Construct(_persistentProgress.Progress.Settings);
            _entityContainer.RegisterEntity(new SettingsPanel(settingsView, _persistentProgress, _saveLoad, _soundService));
            _entityContainer.RegisterEntity(settingsView);
            
            settingsButton.onClick.AddListener(settingsView.SwitchSettingsView);
            return settingsView;
        }

        public void CreateMainMenu(Transform parent) => 
            Object.Instantiate(_staticData.Prefabs.MainMenuPrefab, parent);

        public Button CreatePlayButton(Transform parent) => 
            Object.Instantiate(_staticData.Prefabs.PlayButtonPrefab, parent);

        public GameVariantView[] CreateGameVariants(Transform parent)
        {
            GameVariantView[] views = new GameVariantView[_staticData.GameViews.GameViewsData.Length];
            
            Transform holder = Object.Instantiate(_staticData.Prefabs.GamesHolderPrefab, parent);

            for (int i = 0; i < views.Length; i++)
            {
                GameVariantView view = Object.Instantiate(_staticData.Prefabs.GameVariant, holder);
                view.Construct(_staticData.GameViews.GameViewsData[i]);
                views[i] = view;
            }

            return views;
        }

        public void CreateTimer()
        {
            Transform root = CreateRootCanvas().transform;
            TimerView timerView = Object.Instantiate(_staticData.Prefabs.TimerPrefab, root);
            ITimer timer = new Timer(timerView, _staticData.MemoryGameConfig.RoundTime, _coroutineRunner);
            _entityContainer.RegisterEntity(timer);
        }

        public void CreateResultPopUp(Transform parent)
        {
            ResultPopUpView resultPop = Object.Instantiate(_staticData.Prefabs.ResultPopUpPrefab, parent);
            _entityContainer.RegisterEntity(resultPop);
            resultPop.Hide();
        }

        public void CreateScore(Transform parent)
        {
            ScoreView scoreView = Object.Instantiate(_staticData.Prefabs.ScoreViewPrefab, parent);
            scoreView.SetScoreText(_persistentProgress.Progress.Records.TotalScore);
            _entityContainer.RegisterEntity(scoreView);
        }

        public void CreateBackButton(Transform parent)
        {
            BackButton backButton = Object.Instantiate(_staticData.Prefabs.BackButtonPrefab, parent);
            _entityContainer.RegisterEntity(backButton);
        }

        private Button CreateSettingsButton(Transform parent)
        {
            Button settingsButton = Object.Instantiate(_staticData.Prefabs.SettingsButtonPrefab, parent);
            _entityContainer.RegisterEntity(settingsButton);
            return settingsButton;
        }
    }
}