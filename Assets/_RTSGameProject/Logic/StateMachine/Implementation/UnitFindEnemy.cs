using _RTSGameProject.Logic.Common.Character.Model;
using _RTSGameProject.Logic.Common.Services;
using _RTSGameProject.Logic.StateMachine.Core;

namespace _RTSGameProject.Logic.StateMachine.Implementation
{
    public class UnitFindEnemy : IEnterState
    {
        private readonly Unit _unit;
        private readonly UnitsRepository _unitsRepository;

        public UnitFindEnemy(Unit unit, UnitsRepository unitsRepository)
        {
            _unit = unit;
            _unitsRepository = unitsRepository;
        }

        public void Enter()
        {
            Unit enemy = _unitsRepository.GetClosestEnemy(_unit , _unit.DistanceToFindEnemy);
            _unit.AssignEnemy(enemy);
        }
    }
}