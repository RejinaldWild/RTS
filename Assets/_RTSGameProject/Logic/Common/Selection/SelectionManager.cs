using UnityEngine;
using System.Collections.Generic;
using _RTSGameProject.Logic.Common.Character.Model;
using _RTSGameProject.Logic.Common.Construction.Model;
using Zenject;

namespace _RTSGameProject.Logic.Common.Selection
{
    public class SelectionManager
    {
        private LayerMask _clickable;
        private LayerMask _ground;
        private GameObject GroundMarker;
        
        public List<Unit> SelectedUnits { get; private set; }
        public List<HouseBuilding> SelectedBuildings { get; private set; }

        [Inject]
        public SelectionManager(GameObject groundMarker)
        {
            GroundMarker = groundMarker;
            SelectedBuildings = new List<HouseBuilding>();
            SelectedUnits = new List<Unit>();
        }

        public void Select(RaycastHit hit)
        {
            if (hit.collider.TryGetComponent(out ISelectable selectable) && selectable.Team == 0)
            {
                SelectByClicking(selectable);
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
            if(hit.collider.TryGetComponent(out ISelectable selectable) && selectable.Team == 0)
                MultiSelect(selectable);
        }
        
        public void ShowDragPreselect(ISelectable selectable)
        {
            if (selectable is Unit unit && SelectedUnits.Contains(unit) == false && unit.Team == 0)
            {
               PreselectTriggerSectionIndicator(unit, true);
            }
            
            if (selectable is HouseBuilding building && SelectedBuildings.Contains(building) == false && building.Team == 0)
            {
                PreselectTriggerSectionIndicator(building, true);
            }
        }
        
        public void HidDragPreselect(ISelectable selectable)
        {
            if (selectable is Unit unit && SelectedUnits.Contains(unit) == false && unit.Team == 0)
            {
                PreselectTriggerSectionIndicator(unit, false);
            }
            
            if (selectable is HouseBuilding building && SelectedBuildings.Contains(building) == false && building.Team == 0)
            {
                PreselectTriggerSectionIndicator(building, false);
            }
        }
        
        public void DragSelect(ISelectable selectable)
        {
            if (selectable is Unit unit && SelectedUnits.Contains(unit) == false && unit.Team == 0)
            {
                SelectedUnits.Add(unit);
                SelectSelectable(unit, true);
            }

            if (selectable is HouseBuilding building && SelectedBuildings.Contains(building) == false && building.Team == 0)
            {
                SelectedBuildings.Add(building);
                SelectSelectable(building, true);
                building.SubscribeClickPanel();
                building.ShowUIPanel(true);
            }
        }
    
        public void DeselectAll()
        {
            foreach (var unit in SelectedUnits)
            {
                PreselectTriggerSectionIndicator(unit, false);
                SelectSelectable(unit, false);
            }
            GroundMarker.SetActive(false);
            SelectedUnits.Clear();
            
            foreach (var building in SelectedBuildings)
            {
                PreselectTriggerSectionIndicator(building, false);
                SelectSelectable(building, false);
                building.UnsubscribeClickPanel();
                building.ShowUIPanel(false);
            }
            SelectedBuildings.Clear();
        }
        
        public void MultiSelect(ISelectable selectable)
        {
            if (selectable is Unit unit)
            {
                if (SelectedUnits.Contains(unit) == false)
                {
                    SelectedUnits.Add(unit);
                    SelectSelectable(unit, true);
                }
                else
                {
                    SelectedUnits.Remove(unit);
                    SelectSelectable(unit, false);
                }
            }
            
            if (selectable is HouseBuilding building)
            {
                if (SelectedBuildings.Contains(building) == false)
                {
                    SelectedBuildings.Add(building);
                    SelectSelectable(building, true);
                    building.SubscribeClickPanel();
                    building.ShowUIPanel(true);
                }
                else
                {
                    SelectedBuildings.Remove(building);
                    SelectSelectable(building, false);
                    building.UnsubscribeClickPanel();
                    building.ShowUIPanel(false);
                }
            }
        }
        
        private void SelectSelectable(ISelectable selectable, bool isSelected)
        {
            PreselectTriggerSectionIndicator(selectable, false);
            TriggerSectionIndicator(selectable, isSelected);
            if (selectable is Unit unit)
            {
                EnableUnitMovement(unit, isSelected);
            }
        }

        private void SelectByClicking(ISelectable selectable)
        {
            DeselectAll();
            if (selectable is Unit unit)
            {
                SelectedUnits.Add(unit);
                SelectSelectable(unit, true);
            }

            if (selectable is HouseBuilding building)
            {
                SelectedBuildings.Add(building);
                SelectSelectable(building, true);
                building.SubscribeClickPanel();
                building.ShowUIPanel(true);
            }
        }
        
        private void EnableUnitMovement(Unit unit, bool isMove)
        {
            if (unit != null)
            {
                unit.GetComponent<UnitMovement>().enabled = isMove;
            }
        }

        private void PreselectTriggerSectionIndicator(ISelectable selectable, bool isVisible)
        {
            if (selectable is Unit unit)
            {
                unit.transform.GetChild(1).gameObject.SetActive(isVisible);
            }
            if (selectable is HouseBuilding building)
            {
                building.transform.GetChild(1).gameObject.SetActive(isVisible);
            }
        }
        
        private void TriggerSectionIndicator(ISelectable selectable, bool isVisible)
        {
            if (selectable is Unit unit)
            {
                unit.transform.GetChild(0).gameObject.SetActive(isVisible);
            }
            if (selectable is HouseBuilding building)
            {
                building.transform.GetChild(0).gameObject.SetActive(isVisible);
            }
        }
    }
}
