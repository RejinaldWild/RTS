using _RTSGameProject.Logic.Common.Character.Model;
using _RTSGameProject.Logic.StateMachine.Core;

namespace _RTSGameProject.Logic.StateMachine.Implementation
{
    public class MoveToEnemy : IUpdateState
    {
        private Unit _unit;

        public MoveToEnemy(Unit unit)
        {
            _unit = unit;
        }
        
        public void Update()
        {
            _unit.MoveTo();
        }
    }
}