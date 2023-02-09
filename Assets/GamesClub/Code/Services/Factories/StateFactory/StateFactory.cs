using GamesClub.Code.Infrastructure.StateMachine.States;
using Zenject;

namespace GamesClub.Code.Services.Factories.StateFactory
{
    public class StatesFactory : IStateFactory
    {
        private readonly IInstantiator _instantiator;

        public StatesFactory(IInstantiator instantiator) =>
            _instantiator = instantiator;

        public T Create<T>() where T : IExitableState =>
            _instantiator.Instantiate<T>();
    }
}