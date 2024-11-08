using _RTSGameProject.Logic.Common.Character.Model;
using _RTSGameProject.Logic.StateMachineAI.Core;

namespace _RTSGameProject.Logic.StateMachineAI.Implementation
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