using System;
using GamesClub.Code.Infrastructure.StateMachine.States;

namespace GamesClub.Code.Infrastructure.StateMachine.StateSwitcher
{
    public class StateSwitcher : IStateSwitcher
    {
        public event Action<Type> OnStateSwitched;
        public event Action<Type, object> OnStateSwitchedPayloaded;

        public void SwitchTo<TState>() where TState : class, IState =>
            OnStateSwitched?.Invoke(typeof(TState));

        public void SwitchTo<TState>(object payload) where TState : class, IPayloadedState =>
            OnStateSwitchedPayloaded?.Invoke(typeof(TState), payload);

        public void SwitchTo(Type state) =>
            OnStateSwitched?.Invoke(state);
    }
}