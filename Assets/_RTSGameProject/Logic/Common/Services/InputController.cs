using _RTSGameProject.Logic.Common.Character.Model;
using _RTSGameProject.Logic.Common.Selection;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _RTSGameProject.Logic.Common.Services
{
    public class InputController
    {
        private SelectionManager _selectionManager;
        private SelectionBox _selectionBox;
        private LayerMask _clickable;
        private LayerMask _ground;
        private LayerMask _building;
        private FormationController _formationController;
        private InputCatchKeyClick _inputCatchKeyClick;

        public InputController(InputCatchKeyClick inputCatchKeyClick, SelectionManager selectionManager, 
                                SelectionBox selectionBox, LayerMask clickable, 
                                LayerMask ground, LayerMask building, FormationController formationController)
        {
            _inputCatchKeyClick = inputCatchKeyClick;
            _selectionManager = selectionManager;
            _selectionBox = selectionBox;
            _clickable = clickable;
            _ground = ground;
            _building = building;
            _formationController = formationController;

            _inputCatchKeyClick.OnLeftClickMouseButton += OnLeftClickMouseButtoned;
            _inputCatchKeyClick.OnLeftClickMouseButtonHold += OnLeftClickMouseButtonHolded;
            _inputCatchKeyClick.OnLeftClickMouseButtonUp += OnLeftClickMouseButtonUped;
            _inputCatchKeyClick.OnRightClickMouseButtonDown += OnRightClickMouseButtonDowned;
        }

        public void Unsubscribe()
        {
            _inputCatchKeyClick.OnLeftClickMouseButton -= OnLeftClickMouseButtoned;
            _inputCatchKeyClick.OnLeftClickMouseButtonHold -= OnLeftClickMouseButtonHolded;
            _inputCatchKeyClick.OnLeftClickMouseButtonUp -= OnLeftClickMouseButtonUped;
            _inputCatchKeyClick.OnRightClickMouseButtonDown -= OnRightClickMouseButtonDowned;
        }
        
        private void OnLeftClickMouseButtoned(Ray ray)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                RaycastHit hit;
                _selectionBox.StartPositionSelectionBox();
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, _clickable))
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        _selectionManager.MultiSelect(hit);
                    }
                    else
                    {
                        _selectionManager.Select(hit);
                    }
                }
                else
                {
                    _selectionManager.DeselectAll();
                }
            }
        }
        
        private void OnLeftClickMouseButtonHolded(Ray ray)
        {
            _selectionBox.StartDrawAndSelect();
        }
        
        private void OnLeftClickMouseButtonUped()
        {
            _selectionBox.EndDrawAndSelect();
        }
        
        private void OnRightClickMouseButtonDowned(Ray ray)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                RaycastHit hit;
                if (_selectionManager.SelectedBuildings.Count > 0)
                {
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, _ground))
                    {
                        foreach (var building in _selectionManager.SelectedBuildings)
                        {
                            building.SetRallyPoint(hit.point);
                        }
                    }
                }

                if (_selectionManager.SelectedUnits.Count > 0)
                {
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, _ground))
                    {
                        _selectionManager.ShowGroundMarker(hit.point);
                        _formationController.SetFormationCenter(_selectionManager.SelectedUnits);
                        foreach (var unit in _selectionManager.SelectedUnits)
                        {
                            unit.IsCommandedToAttack = false;
                            unit.IsCommandedToMove = true;
                            unit.RemoveEnemy();
                            unit.Position = hit.point + unit.Position;
                            unit.Move();
                        }
                    }

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, _clickable))
                    {
                        foreach (Unit unit in _selectionManager.SelectedUnits)
                        {
                            if (hit.collider.TryGetComponent(out Unit enemy) && enemy.Team != 0)
                            {
                                unit.IsCommandedToMove = false;
                                unit.IsCommandedToAttack = true;
                                unit.RemoveEnemy();
                                unit.AssignEnemy(enemy);
                                unit.Attack();
                            }
                        }
                    }
                }
            }
        }
    }
}

