using _RTSGameProject.Logic.Common.Character.Model;
using _RTSGameProject.Logic.StateMachine.Core;

namespace _RTSGameProject.Logic.StateMachine.Implementation
{
    public class UnitMoveState: IUpdateState
    {
        private Unit _unit;

        public UnitMoveState(Unit unit)
        {
            _unit = unit;
        }
        
        public void Update()
        {
            _unit.Move();
        }
    }
}
