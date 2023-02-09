using GamesClub.Code.Services.CardOpener;
using GamesClub.Code.Services.CoroutineRunner;
using GamesClub.Code.Services.EntityContainer;
using GamesClub.Code.Services.Factories.ClawCraneFactory;
using GamesClub.Code.Services.Factories.GameFactory;
using GamesClub.Code.Services.Factories.UIFactory;
using GamesClub.Code.Services.InputSystem;
using GamesClub.Code.Services.PersistentProgress;
using GamesClub.Code.Services.Physics;
using GamesClub.Code.Services.SaveLoad;
using GamesClub.Code.Services.SceneLoader;
using GamesClub.Code.Services.ScoreService;
using GamesClub.Code.Services.SoundService;
using GamesClub.Code.Services.StaticData;
using GamesClub.Code.Services.StaticData.StaticDataProvider;
using UnityEngine;
using Zenject;

namespace GamesClub.Code.Infrastructure.Installers
{
    public class ServiceInstaller : MonoInstaller, ICoroutineRunner
    {
        [SerializeField] private SoundService SoundService;
        
        public override void InstallBindings()
        {
            RegisterSceneLoader();
            RegisterStaticDataProvider();
            RegisterCoroutineRunner();
            RegisterEntityContainer();
            RegisterSaveLoad();
            RegisterPersistentProgress();
            RegisterStaticData();
            RegisterUIFactory();
            RegisterClawCraneFactory();
            RegisterGameFactory();
            RegisterInput();
            RegisterRaycaster();
            RegisterCardOpener();
            RegisterScoreService();
            RegisterSoundService();
        }

        private void RegisterSoundService() => 
            Container.Bind<ISoundService>().FromInstance(SoundService).AsSingle();

        private void RegisterSceneLoader() =>
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();

        private void RegisterStaticDataProvider() =>
            Container.Bind<IStaticDataProvider>().To<StaticDataProvider>().AsSingle();

        private void RegisterCoroutineRunner() =>
            Container.Bind<ICoroutineRunner>().FromInstance(this).AsSingle();

        private void RegisterEntityContainer() =>
            Container.BindInterfacesTo<EntityContainer>().AsSingle();

        private void RegisterSaveLoad() =>
            Container.Bind<ISaveLoad>().To<PrefsSaveLoad>().AsSingle();

        private void RegisterPersistentProgress() =>
            Container.Bind<IPersistentProgress>().To<PersistentPlayerProgress>().AsSingle();

        private void RegisterStaticData() =>
            Container.Bind<IStaticData>().To<StaticData>().AsSingle();
        
        private void RegisterUIFactory() =>
            Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();
        
        private void RegisterClawCraneFactory() =>
            Container.Bind<IClawCraneFactory>().To<ClawCraneFactory>().AsSingle();
        
        private void RegisterGameFactory() =>
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
        
        private void RegisterInput() =>
            Container.Bind<IInput>().To<MobileInput>().AsSingle();
        
        private void RegisterRaycaster() =>
            Container.Bind<IRaycaster>().To<Raycaster>().AsSingle();
        
        private void RegisterCardOpener() =>
            Container.Bind<ICardOpener>().To<CardOpener>().AsSingle();

        private void RegisterScoreService() =>
            Container.Bind<IScoreService>().To<ScoreService>().AsSingle();
    }
}