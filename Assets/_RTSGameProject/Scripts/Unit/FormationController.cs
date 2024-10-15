using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

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
    
    private void RemoveSelectedUnitsFormation(Unit unit)
    {
        _agents.Remove(unit.Agent);
    }

    public void SetFormationCenter(Vector3 center)
    {
        foreach (var element in _unitSelectionManager.selectedUnits)
        {
            OnUnitChangedPosition?.Invoke(element.position);
        }
        
        Vector3[] positions = _generator.GetPosition(_agents.Count);
        for (int i = 0; i < positions.Length; i++)
        {
            _unitSelectionManager.selectedUnits[i].position = positions[i];
        }
    }

    private void AddSelectedUnitsFormation(Unit unit)
    {
        _agents.Add(unit.Agent);
    }
}
