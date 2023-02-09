using GamesClub.Code.Core.UI.BackButton;
using GamesClub.Code.Core.UI.Score;
using GamesClub.Code.Infrastructure.StateMachine.StateSwitcher;
using GamesClub.Code.Services.EntityContainer;
using GamesClub.Code.Services.Factories.UIFactory;
using GamesClub.Code.Services.SceneLoader;
using GamesClub.Code.Services.SoundService;
using UnityEngine;
using UnityEngine.UI;

namespace GamesClub.Code.Infrastructure.StateMachine.States
{
    public class MenuState : IState
    {
        private readonly IStateSwitcher _stateSwitcher;
        private readonly ISceneLoader _sceneLoader;
        private readonly IEntityContainer _entityContainer;
        private readonly IUIFactory _uiFactory;
        private readonly ISoundService _soundService;
        private Button _playButton;
        private Button _statisticButton;

        private const string MenuScene = "Menu";

        public MenuState(IStateSwitcher stateSwitcher, ISceneLoader sceneLoader, IEntityContainer entityContainer, IUIFactory uiFactory, ISoundService soundService)
        {
            _stateSwitcher = stateSwitcher;
            _sceneLoader = sceneLoader;
            _entityContainer = entityContainer;
            _uiFactory = uiFactory;
            _soundService = soundService;
        }

        public void Enter()
        {
            _sceneLoader.LoadScene(MenuScene, CreateUI);
            SetupTopPanel();
            _soundService.EnableBackgroundMusic();
        }

        private void SetupTopPanel()
        {
            _entityContainer.GetEntity<BackButton>().Hide();
            _entityContainer.GetEntity<ScoreView>().Hide();
        }

        private void CreateUI()
        {
            Transform root = _uiFactory.CreateRootCanvas().transform;
            _uiFactory.CreateMainMenu(root);
            
            _playButton = _uiFactory.CreatePlayButton(root);
            _playButton.onClick.AddListener(SwitchState);
        }

        private void SwitchState() => _stateSwitcher.SwitchTo<ChooseGameState>();

        public void Exit()
        {
            _playButton.onClick.RemoveListener(SwitchState);
        }
    }
}