using GamesClub.Code.Infrastructure.StateMachine.StateSwitcher;
using GamesClub.Code.Services.Factories.UIFactory;
using UnityEngine;

namespace GamesClub.Code.Infrastructure.StateMachine.States
{
    public class LoadPersistentEntityState : IState
    {
        private readonly IStateSwitcher _stateSwitcher;
        private readonly IUIFactory _uiFactory;

        public LoadPersistentEntityState(IStateSwitcher stateSwitcher, IUIFactory uiFactory)
        {
            _stateSwitcher = stateSwitcher;
            _uiFactory = uiFactory;
        }
        public void Enter()
        {
            Transform rootCanvas = CreateRootCanvas().transform;
            CreatePersistentEntities(rootCanvas);
            _stateSwitcher.SwitchTo<MenuState>();
        }

        private void CreatePersistentEntities(Transform rootCanvas)
        {
            _uiFactory.CreateSettings(rootCanvas);
            _uiFactory.CreateScore(rootCanvas);
            _uiFactory.CreateBackButton(rootCanvas);
        }

        private GameObject CreateRootCanvas()
        {
            Canvas rootCanvas = _uiFactory.CreateRootCanvas().GetComponent<Canvas>();
            rootCanvas.sortingOrder = 10;
            Object.DontDestroyOnLoad(rootCanvas);
            return rootCanvas.gameObject;
        }

        public void Exit()
        {
            
        }
    }
}