using System;
using System.Linq;
using _RTSGameProject.Logic.Common.AI;
using _RTSGameProject.Logic.Common.Character.Model;

namespace _RTSGameProject.Logic.StateMachine.Core
{
    public class StateMachine: IAiActor
    {
        private IState _currentState;
        private readonly IState[] _states;
        private readonly Transition[] _transitions;
        private readonly Unit _unit;

        public StateMachine(IState originState, IState[] states, Transition[] transitions)
        {
            _states = states;
            _transitions = transitions;
            _currentState = originState;
        }

        public void Update()
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