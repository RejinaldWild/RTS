using _RTSGameProject.Logic.Common.Character.Model;
using _RTSGameProject.Logic.StateMachine.Core;

namespace _RTSGameProject.Logic.StateMachine.Implementation
{
    public class UnitPatrolling : IUpdateState
    {
        private Unit _unit;
        public UnitPatrolling(Unit unit)
        {
            _unit = unit;
        }

        public void Update()
        {
            _unit.Patrolling();
        }
    }
}