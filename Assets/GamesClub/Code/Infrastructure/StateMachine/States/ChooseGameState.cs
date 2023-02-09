using System;
using GamesClub.Code.Core.UI.BackButton;
using GamesClub.Code.Core.UI.GameVariant;
using GamesClub.Code.Core.UI.Score;
using GamesClub.Code.Infrastructure.StateMachine.StateSwitcher;
using GamesClub.Code.Services.EntityContainer;
using GamesClub.Code.Services.Factories.UIFactory;
using GamesClub.Code.Services.SceneLoader;
using UnityEngine;

namespace GamesClub.Code.Infrastructure.StateMachine.States
{
    public class ChooseGameState : IState
    {
        private const string ChooseGameScene = "ChooseGame";
        private readonly ISceneLoader _sceneLoader;
        private readonly IUIFactory _uiFactory;
        private readonly IStateSwitcher _stateSwitcher;
        private readonly IEntityContainer _entityContainer;
        private GameVariantView[] _gameViews;

        public ChooseGameState(ISceneLoader sceneLoader, IUIFactory uiFactory, IStateSwitcher stateSwitcher, IEntityContainer entityContainer)
        {
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
            _stateSwitcher = stateSwitcher;
            _entityContainer = entityContainer;
        }

        public void Enter()
        {
            _sceneLoader.LoadScene(ChooseGameScene, CreateUI);
            SetupTopPanel();
        }
        
        private void SetupTopPanel()
        {
            _entityContainer.GetEntity<BackButton>().Show();
            _entityContainer.GetEntity<ScoreView>().Hide();

            _entityContainer.GetEntity<BackButton>().OnBackButton += ChangeState;
        }
        
        private void ChangeState() => _stateSwitcher.SwitchTo<MenuState>();

        private void CreateUI()
        {
            Transform root = _uiFactory.CreateRootCanvas().transform;
            _gameViews = _uiFactory.CreateGameVariants(root);

            SubscribeGameButtons();
        }

        public void Exit()
        {
            UnSubscribeGameButtons();
            _entityContainer.GetEntity<BackButton>().OnBackButton -= ChangeState;
        }

        private void SubscribeGameButtons()
        {
            foreach (GameVariantView view in _gameViews) 
                view.OnPlay += MoveToGameState;
        }

        private void UnSubscribeGameButtons()
        {
            foreach (GameVariantView view in _gameViews) 
                view.OnPlay -= MoveToGameState;
        }

        private void MoveToGameState(Type state) => _stateSwitcher.SwitchTo(state);
    }
}