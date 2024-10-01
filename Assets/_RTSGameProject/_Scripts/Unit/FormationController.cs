using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FormationController : MonoBehaviour
{
    [SerializeField] private UnitSelectionManager _unitSelectionManager;
    [SerializeField] private UnitMovement[] _unitMovements;
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
        _unitSelectionManager.OnDeselectedUnits += RemoveSelectedUnitsFormation;
    }

    private void MoveFormation(Vector3 point)
    {
        SetFormationCenter(point);
    }

    private void RemoveSelectedUnitsFormation()
    {
        _agents.Clear();
        foreach (var unit in _unitMovements)
        {
            unit.OnUnitChangedPosition -= MoveFormation;
        }
    }

    public void SetFormationCenter(Vector3 center)
    {
        Vector3[] positions = _generator.GetPosition(_agents.Count);
        for (int i = 0; i < positions.Length; i++)
        {
            _agents[i].SetDestination(center + (positions[i] * 5));
        }
    }

    private void AddSelectedUnitsFormation()
    {
        foreach (var unit in _unitSelectionManager.selectedUnits)
        {
            if (_agents.Contains(unit.GetComponent<NavMeshAgent>()))
            {
                
            }
            else
            {
                _agents.Add(unit.GetComponent<NavMeshAgent>());
                foreach (var i in _unitMovements)
                {
                    i.OnUnitChangedPosition += MoveFormation;
                }
            }
        }
    }
}
