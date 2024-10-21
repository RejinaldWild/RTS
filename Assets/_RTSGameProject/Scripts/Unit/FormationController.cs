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
    
        private IFormationPositionGenerator _generator;
        [field: SerializeField] public bool IsManySelected { get; private set; }
    
        private void Start()
        {
            _generator = GetComponent<IFormationPositionGenerator>();
        }

        public void SetFormationCenter(Vector3 center)
        {
            IsManySelected = _unitSelectionManager.IsMultiselectUnit;
            if (IsManySelected)
            {
                Vector3[] positions = _generator.GetPosition(_unitSelectionManager.SelectedUnits.Count);
                for (int i = 0; i < positions.Length; i++)
                {
                    _unitSelectionManager.SelectedUnits[i].Position = positions[i];
                }
                
                foreach (var element in _unitSelectionManager.SelectedUnits)
                {
                    OnUnitChangedPosition?.Invoke(element.Position);
                }
            }
            else
            {
                for (int i = 0; i < _unitSelectionManager.SelectedUnits.Count; i++)
                {
                    _unitSelectionManager.SelectedUnits[i].Position = center;
                }
                OnUnitChangedPosition?.Invoke(center);
            }
        }
    }
}
