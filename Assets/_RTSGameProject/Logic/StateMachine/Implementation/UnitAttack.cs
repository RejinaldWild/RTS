using _RTSGameProject.Logic.Common.Character.Model;
using _RTSGameProject.Logic.StateMachineAI.Core;

namespace _RTSGameProject.Logic.StateMachineAI.Implementation
{
    public class UnitAttack : IEnterState
    {
        private Unit _unit;

        public UnitAttack(Unit unit)
        {
            _unit = unit;
        }
        public void Enter()
        {
            _unit.Attack();
        }
    }
}