using _RTSGameProject.Logic.Common.Services;
using UnityEngine;

namespace _RTSGameProject.Logic.Common.Character.Model
{
    public class UnitFindEnemy
    {
        private UnitsRepository _unitsRepository;
        private float _distance;
        
        public UnitFindEnemy (float distance)
        {
            _distance = distance;
        }
        
        public void FindEnemy(Unit unit, UnitsRepository unitsRepository)
        {
            Unit enemy = null;
            enemy = unitsRepository.GetClosestEnemy(unit, _distance);
            unit.AssignEnemy(enemy);
        }
    }
}