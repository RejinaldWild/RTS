using System;
using System.Collections.Generic;
using _RTSGameProject.Logic.Common.Character.Model;
using UnityEngine;

namespace _RTSGameProject.Logic.Common.Services
{
    public class UnitRepository
    {
        public List<Unit> AllUnits;
        private Health _health;

        public UnitRepository(Transform unitListParent)
        {
            AllUnits = new List<Unit>();
            foreach (Transform unitChild in unitListParent)
            {
                if (unitChild.TryGetComponent(out Unit unit))
                {
                    Register(unit);
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

        public Unit GetClosestEnemy(Unit unit, float maxDistance)
        {
            float closestSqrDistance = maxDistance * maxDistance;
            Unit closestEnemy = null;

            foreach (Unit forUnit in AllUnits)
            {
                float sqrDistance = Vector3.SqrMagnitude(forUnit.Position - unit.Position);

                if (unit.Team != forUnit.Team && sqrDistance <= closestSqrDistance)
                {
                    closestSqrDistance = sqrDistance;
                    closestEnemy = forUnit;
                }
            }

            if (closestEnemy != null)
                return closestEnemy;
            else 
                return null;
        }

        public void Update()
        {
            foreach (Unit unit in AllUnits)
            {
                if (unit.Health.Current <= 0)
                {
                    Unregister(unit);
                }
            }
        }
        
        public void Register(Unit unit) => 
            AllUnits.Add(unit);

        public void Unregister(Unit unit) => 
            AllUnits.Remove(unit);
    }
}
