using System.Collections.Generic;
using UnityEngine;

namespace RTS.Scripts
{
    public class UnitSelectionManager
    {
        private LayerMask _clickable;
        private LayerMask _ground;
        private GameObject GroundMarker;
    
        public List<Unit> AllUnits { get; private set; }
        public List<Unit> SelectedUnits { get; private set; }

        public UnitSelectionManager(GameObject groundMarker)
        {
            AllUnits = new List<Unit>();
            AllUnits.AddRange(GameObject.FindObjectsOfType<Unit>());
            SelectedUnits = new List<Unit>();
            GroundMarker = groundMarker;
        }

        public void Select(RaycastHit hit)
        {
            hit.collider.TryGetComponent(out Unit unit);
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
            MultiSelect(unit);
        }
        
        public void DragSelect(Unit unit)
        {
            if (SelectedUnits.Contains(unit) == false)
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
