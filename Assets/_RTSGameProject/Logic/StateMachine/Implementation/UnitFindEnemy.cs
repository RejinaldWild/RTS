using _RTSGameProject.Logic.Common.Character.Model;
using _RTSGameProject.Logic.Common.Services;
using _RTSGameProject.Logic.StateMachine.Core;

namespace _RTSGameProject.Logic.StateMachine.Implementation
{
    public class UnitFindEnemy : IEnterState
    {
        private readonly Unit _unit;
        private readonly UnitRepository _unitRepository;

        public UnitFindEnemy(Unit unit, UnitRepository unitRepository)
        {
            _unit = unit;
            _unitRepository = unitRepository;
        }

        public void Enter()
        {
            Unit enemy = _unitRepository.GetClosestEnemy(_unit , _unit.DistanceToFindEnemy);
            _unit.AssignEnemy(enemy);
        }
    }
}