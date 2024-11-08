using System.Collections.Generic;
using _RTSGameProject.Logic.Common.Character.Model;
using UnityEngine;

namespace _RTSGameProject.Logic.Common.Selection
{
    public class UnitSelectionManager
    {
        private LayerMask _clickable;
        private LayerMask _ground;
        private GameObject GroundMarker;
        private Transform _unitListParent;
    
        public List<Unit> AllUnits { get; private set; }
        public List<Unit> SelectedUnits { get; private set; }

        public UnitSelectionManager(Transform unitListParent, GameObject groundMarker)
        {
            _unitListParent = unitListParent;
            GroundMarker = groundMarker;
            AllUnits = new List<Unit>();
            SelectedUnits = new List<Unit>();
            
            foreach (Transform unitChild in _unitListParent)
            {
                if (unitChild.TryGetComponent(out Unit unit))
                {
                    AllUnits.Add(unit);
                }
            }
        }

        public void Select(RaycastHit hit)
        {
            hit.collider.TryGetComponent(out Unit unit);
            if(unit.Team == 0)
                SelectByClicking(unit);
        }
        
        public void ShowGroundMarker(Vector3 point)
        {
            GroundMarker.transform.position = point + new Vector3(0, 0.1f, 0);
            GroundMarker.SetActive(false);
            GroundMarker.SetActive(true);
        }
        
        public void MultiSelectUnits(RaycastHit hit)
        {
            hit.collider.TryGetComponent(out Unit unit);
            if(unit.Team == 0)
                MultiSelect(unit);
        }
        
        public void DragSelect(Unit unit)
        {
            if (SelectedUnits.Contains(unit) == false && unit.Team == 0)
            {
                SelectedUnits.Add(unit);
                SelectUnit(unit, true);
            }
        }
    
        public void DeselectAll()
        {
            foreach (var unit in SelectedUnits)
            {
                SelectUnit(unit, false);
            }
            GroundMarker.SetActive(false);
            SelectedUnits.Clear();
        }
        
        public void MultiSelect(Unit unit)
        {
            if (SelectedUnits.Contains(unit) == false)
            {
                SelectedUnits.Add((unit));
                SelectUnit(unit, true);
            }
            else
            {
                SelectedUnits.Remove(unit);
                SelectUnit(unit, false);
            }
        }
        
        private void SelectUnit(Unit unit, bool isSelected)
        {
            TriggerSectionIndicator(unit, isSelected);
            EnableUnitMovement(unit, isSelected);
        }
        
        private void SelectByClicking(Unit unit)
        {
            DeselectAll();
            SelectedUnits.Add(unit);
            SelectUnit(unit, true);
        }

        private void EnableUnitMovement(Unit unit, bool isMove)
        {
            unit.GetComponent<UnitMovement>().enabled = isMove;
        }

        private void TriggerSectionIndicator(Unit unit, bool isVisible)
        {
            unit.transform.GetChild(0).gameObject.SetActive(isVisible);
        }
    }
}
