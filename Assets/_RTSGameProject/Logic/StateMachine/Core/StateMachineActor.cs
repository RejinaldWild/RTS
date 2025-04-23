using System;
using System.Linq;
using _RTSGameProject.Logic.Common.AI;
using _RTSGameProject.Logic.Common.Character.Model;
using _RTSGameProject.Logic.Common.Services;
using _RTSGameProject.Logic.StateMachine.Implementation;

namespace _RTSGameProject.Logic.StateMachine.Core
{
    public class StateMachineActor: IAiActor
    {
        private IState _currentState;
        private readonly IState[] _states;
        private readonly Transition[] _transitions;
        private readonly Unit _unit;
        private PauseGame _pauseGame;
        
        public StateMachineActor(UnitIdle originState, PauseGame pauseGame, IState[] states, Transition[] transitions)
        {
            _states = states;
            _transitions = transitions;
            _currentState = originState;
            _pauseGame = pauseGame;
        }

        public void Update()
        {
            if (_pauseGame.OnPaused)
            {
                
            }
            else
            {
                foreach (Transition transition in _transitions)
                {
                    if (transition.Condition())
                        TranslateTo(transition.To);
                }
                
                if (_currentState is IUpdateState updateState)
                {
                    updateState.Update();
                }
            }
        }

        private void TranslateTo(Type targetType)
        {
            if (_currentState is IExitState exitState)
            {
                exitState.Exit();
            }
            _currentState = _states.First(x => x.GetType() == targetType);

            if (_currentState is IEnterState enterState)
            {
                enterState.Enter();
            }
        }
    }
}