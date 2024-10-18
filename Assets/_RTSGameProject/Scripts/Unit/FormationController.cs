using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RTS.Scripts
{
    public class FormationController : MonoBehaviour
    {
        public event Action<Vector3> OnUnitChangedPosition;
    
        [SerializeField] private UnitSelectionManager _unitSelectionManager;
        [SerializeField] private List<NavMeshAgent> _agents;
    
        private IFormationPositionGenerator _generator;
    
        private void Start()
        {
            _generator = GetComponent<IFormationPositionGenerator>();
            _unitSelectionManager.OnSelectedUnits += AddSelectedUnitsFormation;
            _unitSelectionManager.OnDeselectedUnits += RemoveSelectedUnitsFormation;
        }
        private void OnDestroy()
        {
            _unitSelectionManager.OnSelectedUnits -= AddSelectedUnitsFormation;
            _unitSelectionManager.OnDeselectedUnits -= RemoveSelectedUnitsFormation;
        }

        public void SetFormationCenter(Vector3 center)
        {
            foreach (var element in _unitSelectionManager.SelectedUnits)
            {
                OnUnitChangedPosition?.Invoke(element.Position);
            }
        
            Vector3[] positions = _generator.GetPosition(_agents.Count);
            for (int i = 0; i < positions.Length; i++)
            {
                _unitSelectionManager.SelectedUnits[i].Position = positions[i];
            }
        }

        private void AddSelectedUnitsFormation(Unit unit)
        {
            _agents.Add(unit.Agent);
        }
    
        private void RemoveSelectedUnitsFormation(Unit unit)
        {
            _agents.Remove(unit.Agent);
        }
    }
}
