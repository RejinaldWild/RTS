﻿using System.Collections.Generic;
using UnityEngine;

namespace _RTSGameProject.Logic.Common.Character.Model
{
    internal class PatrollMovement: MonoBehaviour
    {
        [field: SerializeField] public List<GameObject> Positions{ get; private set; }
        private Vector3 delta = new(0.1f, 0, 0.1f);
        
        public void Move(Unit unit, UnitMovement unitMovement)
        {
            Vector3 currentPosition = unit.transform.position;
            Vector3 targetPosition = Positions[unit.CurrentPositionIndex].transform.position;
    
            if (Vector3.Distance(currentPosition, targetPosition) > delta.magnitude)
            {
                unitMovement.Move(targetPosition);
            }
            else
            {
                unit.CurrentPositionIndex++;
                
                if (unit.CurrentPositionIndex >= Positions.Count)
                {
                    unit.CurrentPositionIndex = 0;
                }
            }
        }
    }
}