using GamesClub.Code.Core.UI.BackButton;
using GamesClub.Code.Core.UI.Score;
using GamesClub.Code.Infrastructure.StateMachine.StateSwitcher;
using GamesClub.Code.Services.EntityContainer;
using GamesClub.Code.Services.Factories.ClawCraneFactory;
using GamesClub.Code.Services.SceneLoader;
using UnityEngine;

namespace GamesClub.Code.Infrastructure.StateMachine.States.ClawCrane
{
    public class ClawCraneGameCreationState : IState
    {
        private const string ClawCraneScene = "ClawCrane";
        private readonly IStateSwitcher _stateSwitcher;
        private readonly ISceneLoader _sceneLoader;
        private readonly IEntityContainer _entityContainer;
        private readonly IClawCraneFactory _factory;

        public ClawCraneGameCreationState(IStateSwitcher stateSwitcher, ISceneLoader sceneLoader, IEntityContainer entityContainer, IClawCraneFactory factory)
        {
            _stateSwitcher = stateSwitcher;
            _sceneLoader = sceneLoader;
            _entityContainer = entityContainer;
            _factory = factory;
        }
        public void Enter()
        {
            _sceneLoader.LoadScene(ClawCraneScene, CreateGame);
            SetupTopPanel();
        }
        
        private void SetupTopPanel()
        {
            _entityContainer.GetEntity<BackButton>().Show();
            _entityContainer.GetEntity<ScoreView>().Show();
        }
        
        private void CreateGame()
        {
            Transform root = _factory.CreateUIRoot().transform;
            _factory.CreateControllPanelView(root);
            _factory.CreateClawCrane();
            _factory.CreateWinPopUp(root);
            MoveToGameLoopState();
        }

        private void MoveToGameLoopState() => 
            _stateSwitcher.SwitchTo<ClawCraneGameLoopState>();

        public void Exit()
        {
        }
    }
}