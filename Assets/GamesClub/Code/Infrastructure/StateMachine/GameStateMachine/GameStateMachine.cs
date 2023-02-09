using System;
using System.Collections.Generic;
using GamesClub.Code.Extensions;
using GamesClub.Code.Infrastructure.StateMachine.States;
using GamesClub.Code.Infrastructure.StateMachine.States.ClawCrane;
using GamesClub.Code.Infrastructure.StateMachine.States.MemoryGame;
using GamesClub.Code.Infrastructure.StateMachine.StateSwitcher;
using GamesClub.Code.Services.Factories.StateFactory;
using UnityEngine;

namespace GamesClub.Code.Infrastructure.StateMachine.GameStateMachine
{
    public class GameStateMachine : IGameStateMachine, IDisposable
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private readonly IStateSwitcher _stateSwitcher;

        private IExitableState _activeState;

        public GameStateMachine(IStateFactory stateFactory, IStateSwitcher stateSwitcher)
        {
            _stateSwitcher = stateSwitcher;
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(LoadProgressState)] = stateFactory.Create<LoadProgressState>(),
                [typeof(LoadPersistentEntityState)] = stateFactory.Create<LoadPersistentEntityState>(),
                [typeof(ChooseGameState)] = stateFactory.Create<ChooseGameState>(),
                [typeof(MemoryGameCreationState)] = stateFactory.Create<MemoryGameCreationState>(),
                [typeof(MemoryGameLoopState)] = stateFactory.Create<MemoryGameLoopState>(),
                [typeof(MemoryGameResultsState)] = stateFactory.Create<MemoryGameResultsState>(),
                [typeof(ClawCraneGameCreationState)] = stateFactory.Create<ClawCraneGameCreationState>(),
                [typeof(ClawCraneGameLoopState)] = stateFactory.Create<ClawCraneGameLoopState>(),
                [typeof(ClawCraneResultsState)] = stateFactory.Create<ClawCraneResultsState>(),
                [typeof(MenuState)] = stateFactory.Create<MenuState>()
            };

            PlayerPrefs.SetString("States", DataExtensions.ToJson(_states));
            
            _stateSwitcher.OnStateSwitched += Enter;
            _stateSwitcher.OnStateSwitchedPayloaded += EnterPayload;
        }

        public void Dispose()
        {
            _stateSwitcher.OnStateSwitched -= Enter;
            _stateSwitcher.OnStateSwitchedPayloaded -= EnterPayload;
            _activeState.Exit();
        }

        private void Enter(Type enterState)
        {
            IExitableState activeState = ChangeState(enterState);
            if(activeState is IState state) state.Enter();
        }

        private void EnterPayload(Type enterState, object payload)
        {
            IExitableState activeState = ChangeState(enterState);
            if(activeState is IPayloadedState state) state.Enter(payload);
        }

        private IExitableState ChangeState(Type enterState)
        {
            _activeState?.Exit();
            IExitableState exitableState = _states[enterState];
            _activeState = exitableState;
            return exitableState;
        }
    }
}