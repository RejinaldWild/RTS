using System;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.Scripts
{
    public class UnitSelectionManager : MonoBehaviour
    {
        public event Action<Unit> OnSelectedUnits;
        public event Action<Unit> OnDeselectedUnits;
    
        public List<Unit> AllUnits = new List<Unit>();
        public List<Unit> SelectedUnits = new List<Unit>();
        public LayerMask Clickable;
        public LayerMask Ground;
        public GameObject GroundMarker;
    
        private Camera _camera;
        private FormationController _formationController;
    
        private void Start()
        {
            _camera = Camera.main;
            foreach (var unit in AllUnits)
            {
                unit.OnUnitAwaked += AddUnit;
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, Clickable))
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        hit.collider.TryGetComponent(out Unit unit);
                        MultiSelect(unit);
                    }
                    else
                    {
                        hit.collider.TryGetComponent(out Unit unit);
                        SelectByClicking(unit);
                    }
                }
                else
                {
                    if (Input.GetKey(KeyCode.LeftShift) == false)
                    {
                        DeselectAll();
                    }
                }
            }
        
            if (Input.GetMouseButtonDown(1) && SelectedUnits.Count > 0)
            {
                RaycastHit hit;
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, Ground))
                {
                    GroundMarker.transform.position = hit.point + new Vector3(0 ,0.1f ,0);
                    GroundMarker.SetActive(false);
                    GroundMarker.SetActive(true);
                }
            }
        }

        private void OnDestroy()
        {
            foreach (var unit in AllUnits)
            {
                unit.OnUnitAwaked -= AddUnit;
            }
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
                OnDeselectedUnits?.Invoke(unit);
            }
            GroundMarker.SetActive(false);
            SelectedUnits.Clear();
        }
    
        private void AddUnit(Unit unit)
        {
            AllUnits.Add(unit);
        }
    
        private void SelectUnit(Unit unit, bool isSelected)
        {
            TriggerSectionIndicator(unit, isSelected);
            EnableUnitMovement(unit, isSelected);
            if (isSelected)
            {
                OnSelectedUnits?.Invoke(unit);
            }
            else
            {
                OnDeselectedUnits?.Invoke(unit);
            }
        }
    
        private void MultiSelect(Unit unit)
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