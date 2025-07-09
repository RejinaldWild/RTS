using _RTSGameProject.Logic.Common.Config;
using _RTSGameProject.Logic.Common.Services;

namespace _RTSGameProject.Logic.Common.Character.Model
{
    public class UnitFindEnemy
    {
        private UnitsRepository _unitsRepository;
        private float _distance;
        
        public UnitFindEnemy (ParamConfig paramConfig)
        {
            _distance = paramConfig.DistanceToFindEnemy;
        }
        
        public void FindEnemy(Unit unit, UnitsRepository unitsRepository)
        {
            Unit enemy = unitsRepository.GetClosestEnemy(unit, _distance);
            if (enemy != null)
            {
                unit.AssignEnemy(enemy);
            }
        }
    }
}