using GamesClub.Code.Infrastructure.StateMachine.GameStateMachine;
using GamesClub.Code.Infrastructure.StateMachine.States;
using GamesClub.Code.Infrastructure.StateMachine.StateSwitcher;
using GamesClub.Code.Services.Factories.StateFactory;
using Zenject;

namespace GamesClub.Code.Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller, IInitializable
    {
        public override void InstallBindings()
        {
            RegisterBootstrapInstaller();
            RegisterStateSwitcher();
            RegisterStateFactory();
            RegisterStateMachine();
        }
        
        public void Initialize() =>
            Container
                .Resolve<IStateSwitcher>()
                .SwitchTo<LoadProgressState>();

        private void RegisterBootstrapInstaller() =>
            Container
                .BindInterfacesTo<BootstrapInstaller>()
                .FromInstance(this)
                .AsSingle();

        private void RegisterStateMachine() =>
            Container
                .BindInterfacesTo<GameStateMachine>()
                .AsSingle()
                .NonLazy();
        
        private void RegisterStateSwitcher() =>
            Container
                .Bind<IStateSwitcher>()
                .To<StateSwitcher>()
                .AsSingle()
                .NonLazy();
        
        private void RegisterStateFactory() =>
            Container
                .Bind<IStateFactory>()
                .To<StatesFactory>()
                .AsSingle()
                .NonLazy();
    }
}