using GamesClub.Code.Infrastructure.StateMachine.States;

namespace GamesClub.Code.Services.Factories.StateFactory
{
    public interface IStateFactory
    {
        T Create<T>() where T : IExitableState;
    }
}