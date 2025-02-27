using System;
using System.Collections.Generic;
using _RTSGameProject.Logic.Common.Character.Model;
using _RTSGameProject.Logic.Common.Selection;
using UnityEngine;
using UnityEngine.Assertions;

namespace _RTSGameProject.Logic.Common.Services
{
    public class UnitsRepository
    {
        public event Action OnUnitKill;
        public event Action OnEnemyKill;
        
        public List<Unit> AllUnits;
        private SelectionManager _selectionManager;

        public UnitsRepository(SelectionManager selectionManager)
        {
            AllUnits = new List<Unit>();
            _selectionManager = selectionManager;
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

        public void Unregister(Unit unit)
        {
            if (unit.Team == 0)
            {
                OnUnitKill?.Invoke();
            }

            if (unit.Team == 1)
            {
                OnEnemyKill?.Invoke();
            }
            AllUnits.Remove(unit);
            _selectionManager.SelectedUnits.Remove(unit);
        }
    }
}
