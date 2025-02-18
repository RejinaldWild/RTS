using System.Collections.Generic;
using _RTSGameProject.Logic.Common.Character.Model;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _RTSGameProject.Logic.Common.Selection
{
    public class SelectionManager
    {
        private LayerMask _clickable;
        private LayerMask _ground;
        private GameObject GroundMarker;
        
        public List<Building.Building> SelectedBuildings { get; private set; }
        public List<Unit> SelectedUnits { get; private set; }

        public SelectionManager(GameObject groundMarker)
        {
            GroundMarker = groundMarker;
            SelectedBuildings = new List<Building.Building>();
            SelectedUnits = new List<Unit>();
        }

        public void Select(RaycastHit hit)
        {
            if (hit.collider.TryGetComponent(out Unit unit) && unit.Team == 0)
            {
                SelectByClicking(unit);
            }
            else if (hit.collider.TryGetComponent(out Building.Building building) && building.Team == 0)
            {
                SelectByClicking(building);
                building.ToggleUI(true);
            }
        }
        
        public void ShowGroundMarker(Vector3 point)
        {
            GroundMarker.transform.position = point + new Vector3(0, 0.1f, 0);
            GroundMarker.SetActive(false);
            GroundMarker.SetActive(true);
        }
        
        public void MultiSelect(RaycastHit hit)
        {
            if(hit.collider.TryGetComponent(out Unit unit) && unit.Team == 0)
                MultiSelectUnit(unit);
            else if (hit.collider.TryGetComponent(out Building.Building building) && building.Team == 0)
                MultiSelectBuilding(building);
            
        }
        
        public void ShowDragPreselect(Unit unit)
        {
            if (SelectedUnits.Contains(unit) == false && unit.Team == 0)
            {
                PreselectTriggerSectionIndicator(unit, true);
            }
        }
        
        public void DragSelect(Unit unit)
        {
            if (SelectedUnits.Contains(unit) == false && unit.Team == 0)
            {
                SelectedUnits.Add(unit);
                SelectUnit(unit, true);
            }
        }
        
        public void ShowDragPreselect(Building.Building building)
        {
            if (SelectedBuildings.Contains(building) == false && building.Team == 0)
            {
                PreselectTriggerSectionIndicator(building, true);
            }
        }
        
        public void DragSelect(Building.Building building)
        {
            if (SelectedBuildings.Contains(building) == false && building.Team == 0)
            {
                SelectedBuildings.Add(building);
                SelectBuilding(building, true);
                building.ToggleUI(true);
            }
        }
        
    
        public void DeselectAll()
        {
            foreach (var unit in SelectedUnits)
            {
                PreselectTriggerSectionIndicator(unit, false);
                SelectUnit(unit, false);
            }
            GroundMarker.SetActive(false);
            SelectedUnits.Clear();
            
            foreach (var building in SelectedBuildings)
            {
                PreselectTriggerSectionIndicator(building, false);
                SelectBuilding(building, false);
                building.ToggleUI(false);
            }
            SelectedBuildings.Clear();
        }
        
        public void MultiSelectUnit(Unit unit)
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
        
        public void MultiSelectBuilding(Building.Building building)
        {
            if (SelectedBuildings.Contains(building) == false)
            {
                SelectedBuildings.Add(building);
                SelectBuilding(building, true);
                building.ToggleUI(true);
            }
            else
            {
                SelectedBuildings.Remove(building);
                SelectBuilding(building, false);
                building.ToggleUI(false);
            }
        }
        
        private void SelectUnit(Unit unit, bool isSelected)
        {
            PreselectTriggerSectionIndicator(unit, false);
            TriggerSectionIndicator(unit, isSelected);
            EnableUnitMovement(unit, isSelected);
        }
        
        private void SelectBuilding(Building.Building building, bool isSelected)
        {
            PreselectTriggerSectionIndicator(building, false);
            TriggerSectionIndicator(building, isSelected);
        }
        
        private void SelectByClicking(Unit unit)
        {
            DeselectAll();
            SelectedUnits.Add(unit);
            SelectUnit(unit, true);
        }

        private void SelectByClicking(Building.Building building)
        {
            DeselectAll();
            SelectedBuildings.Add(building);
            SelectBuilding(building, true);
            building.ToggleUI(true);
        }
        
        
        private void EnableUnitMovement(Unit unit, bool isMove)
        {
            if (unit != null)
            {
                unit.GetComponent<UnitMovement>().enabled = isMove;
            }
        }

        private void PreselectTriggerSectionIndicator(Unit unit, bool isVisible)
        {
            if (unit != null)
            {
                unit.transform.GetChild(1).gameObject.SetActive(isVisible);
            }
        }
        
        private void TriggerSectionIndicator(Unit unit, bool isVisible)
        {
            if (unit != null)
            {
                unit.transform.GetChild(0).gameObject.SetActive(isVisible);
            }
        }
        
        private void PreselectTriggerSectionIndicator(Building.Building building, bool isVisible)
        {
            if (building != null)
            {
                building.transform.GetChild(1).gameObject.SetActive(isVisible);
            }
        }
        
        private void TriggerSectionIndicator(Building.Building building, bool isVisible)
        {
            if (building != null)
            {
                building.transform.GetChild(0).gameObject.SetActive(isVisible);
            }
        }
    }
}
