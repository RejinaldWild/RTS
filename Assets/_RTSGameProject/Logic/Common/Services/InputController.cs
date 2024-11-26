using _RTSGameProject.Logic.Common.Character.Model;
using _RTSGameProject.Logic.Common.Selection;
using UnityEngine;

namespace _RTSGameProject.Logic.Common.Services
{
    public class InputController
    {
        private UnitSelectionManager _unitSelectionManager;
        private UnitSelectionBox _unitSelectionBox;
        private LayerMask _clickable;
        private LayerMask _ground;
        private FormationController _formationController;
        private InputCatchKeyClick _inputCatchKeyClick;

        public InputController(InputCatchKeyClick inputCatchKeyClick, UnitSelectionManager unitSelectionManager, 
                                UnitSelectionBox unitSelectionBox, LayerMask clickable, 
                                LayerMask ground, FormationController formationController)
        {
            _inputCatchKeyClick = inputCatchKeyClick;
            _unitSelectionManager = unitSelectionManager;
            _unitSelectionBox = unitSelectionBox;
            _clickable = clickable;
            _ground = ground;
            _formationController = formationController;

            _inputCatchKeyClick.OnLeftClickMouseButtonDown += OnLeftClickMouseButtonDowned;
            _inputCatchKeyClick.OnLeftClickMouseButton += OnLeftClickMouseButtoned;
            _inputCatchKeyClick.OnLeftClickMouseButtonUp += OnLeftClickMouseButtonUped;
            _inputCatchKeyClick.OnRightClickMouseButtonDown += OnRightClickMouseButtonDowned;
        }

        private void OnLeftClickMouseButtonDowned(Ray ray)
        {
            RaycastHit hit;
            _unitSelectionBox.StartPositionSelectionBox();
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _clickable))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    _unitSelectionManager.MultiSelectUnits(hit);
                }
                else
                {
                    _unitSelectionManager.Select(hit);
                }
            }
            else
            {
                _unitSelectionManager.DeselectAll();
            }
        }
        
        private void OnRightClickMouseButtonDowned(Ray ray)
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _ground))
            {
                _unitSelectionManager.ShowGroundMarker(hit.point);
                _formationController.SetFormationCenter(_unitSelectionManager.SelectedUnits);
                foreach (var unit in _unitSelectionManager.SelectedUnits)
                {
                    unit.Position = hit.point + unit.Position;
                }
            }

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _clickable))
            {
                foreach (Unit unit in _unitSelectionManager.SelectedUnits)
                {
                    if (hit.collider.TryGetComponent(out Unit enemy) && enemy.Team != 0)
                    {
                        unit.AssignEnemy(enemy);
                        unit.Attack();
                    }
                }
            }
        }

        private void OnLeftClickMouseButtonUped()
        {
            _unitSelectionBox.EndDrawAndSelect();
        }

        private void OnLeftClickMouseButtoned()
        {
            _unitSelectionBox.StartDrawAndSelect();
        }

        public void Unsubscribe()
        {
            _inputCatchKeyClick.OnLeftClickMouseButtonDown -= OnLeftClickMouseButtonDowned;
            _inputCatchKeyClick.OnLeftClickMouseButton -= OnLeftClickMouseButtoned;
            _inputCatchKeyClick.OnLeftClickMouseButtonUp -= OnLeftClickMouseButtonUped;
            _inputCatchKeyClick.OnRightClickMouseButtonDown -= OnRightClickMouseButtonDowned;
        }
    }
}

