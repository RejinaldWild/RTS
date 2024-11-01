using UnityEngine;

namespace RTS.Scripts
{
    public class InputController
    {
        private UnitSelectionManager _unitSelectionManager;
        private UnitSelectionBox _unitSelectionBox;
        private LayerMask _clickable;
        private LayerMask _ground;
        private FormationController _formationController;
        private InputManager _inputManager;

        public InputController(InputManager inputManager, UnitSelectionManager unitSelectionManager, 
                                UnitSelectionBox unitSelectionBox, LayerMask clickable, 
                                LayerMask ground, FormationController formationController)
        {
            _inputManager = inputManager;
            _unitSelectionManager = unitSelectionManager;
            _unitSelectionBox = unitSelectionBox;
            _clickable = clickable;
            _ground = ground;
            _formationController = formationController;

            _inputManager.OnLeftClickMouseButtonDown += OnLeftClickMouseButtonDowned;
            _inputManager.OnLeftClickMouseButton += OnLeftClickMouseButtoned;
            _inputManager.OnLeftClickMouseButtonUp += OnLeftClickMouseButtonUped;
            _inputManager.OnRightClickMouseButtonDown += OnRightClickMouseButtonDowned;
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
                    unit.Move(hit.point + unit.Position);
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
            _inputManager.OnLeftClickMouseButtonDown -= OnLeftClickMouseButtonDowned;
            _inputManager.OnLeftClickMouseButton -= OnLeftClickMouseButtoned;
            _inputManager.OnLeftClickMouseButtonUp -= OnLeftClickMouseButtonUped;
            _inputManager.OnRightClickMouseButtonDown -= OnRightClickMouseButtonDowned;
        }
    }
}

