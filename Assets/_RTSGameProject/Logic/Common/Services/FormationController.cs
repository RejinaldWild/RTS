using System.Collections.Generic;
using _RTSGameProject.Logic.Common.Character.Model;
using UnityEngine;

namespace _RTSGameProject.Logic.Common.Services
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
