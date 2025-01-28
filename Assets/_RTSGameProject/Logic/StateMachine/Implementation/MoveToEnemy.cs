using _RTSGameProject.Logic.Common.Character.Model;
using _RTSGameProject.Logic.Common.Services;
using _RTSGameProject.Logic.StateMachine.Core;

namespace _RTSGameProject.Logic.StateMachine.Implementation
{
    public class MoveToEnemy : IUpdateState
    {
        private Unit _unit;
        private UnitsRepository _unitsRepository;

        public MoveToEnemy(Unit unit, UnitsRepository unitsRepository)
        {
            _unit = unit;
            _unitsRepository = unitsRepository;
        }
        
        public void Update()
        {
            _unit.MoveTo();
        }
    }
}