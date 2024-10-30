using UnityEngine;

namespace RTS.Scripts
{
    public class InputController
    {
        private Camera _camera;
        private UnitSelectionManager _unitSelectionManager;
        private UnitSelectionBox _unitSelectionBox;
        private LayerMask _clickable;
        private LayerMask _ground;
        private FormationController _formationController;

        public InputController(Camera camera, UnitSelectionManager unitSelectionManager, 
                                UnitSelectionBox unitSelectionBox, LayerMask clickable, 
                                LayerMask ground, FormationController formationController)
        {
            _camera = camera;
            _unitSelectionManager = unitSelectionManager;
            _unitSelectionBox = unitSelectionBox;
            _clickable = clickable;
            _ground = ground;
            _formationController = formationController;
        }

        public void Update()
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            
            if (Input.GetMouseButtonDown(0))
            {
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

            if (Input.GetMouseButton(0))
            {
                _unitSelectionBox.StartDrawAndSelect();
            }

            if (Input.GetMouseButtonUp(0))
            {
                _unitSelectionBox.EndDrawAndSelect();
            }
            
            if (Input.GetMouseButtonDown(1))
            {
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
        }
    }
}

