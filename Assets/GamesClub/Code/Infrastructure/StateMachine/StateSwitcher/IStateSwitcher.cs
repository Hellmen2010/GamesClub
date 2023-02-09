using System;
using GamesClub.Code.Infrastructure.StateMachine.States;

namespace GamesClub.Code.Infrastructure.StateMachine.StateSwitcher
{
    public interface IStateSwitcher
    {
        event Action<Type> OnStateSwitched;
        event Action<Type, object> OnStateSwitchedPayloaded;
        void SwitchTo<TState>() where TState : class, IState;
        void SwitchTo<TState>(object payload) where TState : class, IPayloadedState;
        void SwitchTo(Type state);
    }
}