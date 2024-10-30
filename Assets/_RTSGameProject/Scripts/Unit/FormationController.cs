using System.Collections.Generic;
using UnityEngine;

namespace RTS.Scripts
{
    public class FormationController
    {
        private IFormationPositionGenerator _generator;
    
        public FormationController(IFormationPositionGenerator generator)
        {
            _generator = generator;
        }

        public void SetFormationCenter(List<Unit> units)
        {
            int unitsCount = units.Count;
            Vector3[] positions = _generator.GetPosition(unitsCount);
            
            for (int i = 0; i < unitsCount; i++)
            {
                units[i].Position = positions[i];
            }
        }
    }
}
