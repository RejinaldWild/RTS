using System.Collections.Generic;
using _RTSGameProject.Logic.Common.Character.Model;
using UnityEngine;

namespace _RTSGameProject.Logic.Common.Services
{
    public class UnitRepository
    {
        public List<Unit> AllUnits;

        public UnitRepository(Transform unitListParent)
        {
            AllUnits = new List<Unit>();
            foreach (Transform unitChild in unitListParent)
            {
                if (unitChild.TryGetComponent(out Unit unit))
                {
                    AllUnits.Add(unit);
                }
            }
        }
        
        public bool HasEnemy(Unit forUnit)
        {
            foreach (Unit unit in AllUnits)
            {
                if (unit.Team != forUnit.Team)
                {
                    return true;
                }
            }

            return false;
        }
        
        public void Register(Unit unit) => 
            AllUnits.Add(unit);

        public void Unregister(Unit unit) => 
            AllUnits.Remove(unit);
    }
}
