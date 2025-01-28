using System;
using System.Collections.Generic;
using _RTSGameProject.Logic.Common.Character.Model;
using UnityEngine;
using UnityEngine.Assertions;

namespace _RTSGameProject.Logic.Common.Services
{
    public class UnitsRepository
    {
        public List<Unit> AllUnits;
        private Health _health;

        public UnitsRepository()
        {
            AllUnits = new List<Unit>();
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
            float closestSqrDistance = maxDistance*maxDistance;
            Unit closestEnemy = null;
            
            foreach (Unit forUnit in AllUnits)
            {
                float sqrDistance = Vector3.SqrMagnitude(forUnit.transform.position - unit.transform.position);
                if (unit.Team != forUnit.Team && sqrDistance <= closestSqrDistance)
                {
                    closestSqrDistance = sqrDistance;
                    closestEnemy = forUnit;
                }
            }

            //Assert.IsNotNull(closestEnemy);
            
            return closestEnemy;
        }
        
        public void Register(Unit unit) => 
            AllUnits.Add(unit);

        public void Unregister(Unit unit) => 
            AllUnits.Remove(unit);
    }
}
