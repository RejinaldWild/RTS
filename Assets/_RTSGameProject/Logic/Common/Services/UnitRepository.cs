using System.Collections.Generic;
using _RTSGameProject.Logic.Common.Character.Model;

namespace _RTSGameProject.Logic.Common.Services
{
    public class UnitRepository
    {
        private List<Unit> _units;

        public bool HasEnemy(Unit forUnit)
        {
            foreach (Unit unit in _units)
            {
                if (unit.Team != forUnit.Team)
                {
                    return true;
                }
            }

            return false;
        }
        
        public void Register(Unit unit) => 
            _units.Add(unit);

        public void Unregister(Unit unit) => 
            _units.Remove(unit);
    }
}
