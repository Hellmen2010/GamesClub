using GamesClub.Code.Core.ClawCraneGame.ClawCrane;
using GamesClub.Code.Core.ClawCraneGame.Joystick;
using GamesClub.Code.Core.ClawCraneGame.UI;
using GamesClub.Code.Services.CoroutineRunner;
using GamesClub.Code.Services.EntityContainer;
using GamesClub.Code.Services.Factories.UIFactory;
using GamesClub.Code.Services.SoundService;
using GamesClub.Code.Services.StaticData;
using UnityEngine;

namespace GamesClub.Code.Services.Factories.ClawCraneFactory
{
    public class ClawCraneFactory : IClawCraneFactory
    {
        private readonly IEntityContainer _entityContainer;
        private readonly IStaticData _staticData;
        private readonly IUIFactory _uiFactory;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly ISoundService _soundService;

        public ClawCraneFactory(IEntityContainer entityContainer, IStaticData staticData, IUIFactory uiFactory, ICoroutineRunner coroutineRunner, ISoundService soundService)
        {
            _entityContainer = entityContainer;
            _staticData = staticData;
            _uiFactory = uiFactory;
            _coroutineRunner = coroutineRunner;
            _soundService = soundService;
        }
        
        public void CreateControllPanelView(Transform parent)
        {
            ClawCraneControllView view = Object.Instantiate(_staticData.Prefabs.clawCraneControllPrefab, parent);
            _entityContainer.RegisterEntity(view);
        }

        public void CreateWinPopUp(Transform parent)
        {
            ClawCraneWinPopUp popUp = Object.Instantiate(_staticData.Prefabs.clawCraneClawCraneWinPopUpPrefab, parent);
            _entityContainer.RegisterEntity(popUp);
        }

        public void CreateClawCrane()
        {
            JoystickView joystick = Object.Instantiate(_staticData.Prefabs.JoystickPrefab);
            ClawCraneView view = Object.Instantiate(_staticData.Prefabs.ClawCranePrefab);
            IClawCraneMover clawCraneMover = new ClawCraneMover(view, _staticData, _coroutineRunner, joystick, _soundService);
            _entityContainer.RegisterEntity(clawCraneMover);
        }

        public Transform CreateUIRoot() => 
            _uiFactory.CreateRootCanvas().transform;
    }
}