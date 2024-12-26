using _RTSGameProject.Logic.Common.Character.Model;
using _RTSGameProject.Logic.Common.Selection;
using UnityEngine;
using UnityEngine.VFX;

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
        private Spawner _spawner;

        public InputController(InputCatchKeyClick inputCatchKeyClick, UnitSelectionManager unitSelectionManager, 
                                UnitSelectionBox unitSelectionBox, LayerMask clickable, 
                                LayerMask ground, FormationController formationController, Spawner spawner)
        {
            _inputCatchKeyClick = inputCatchKeyClick;
            _unitSelectionManager = unitSelectionManager;
            _unitSelectionBox = unitSelectionBox;
            _clickable = clickable;
            _ground = ground;
            _formationController = formationController;
            _spawner = spawner;

            _inputCatchKeyClick.OnLeftClickMouseButtonDown += OnLeftClickMouseButtonDowned;
            _inputCatchKeyClick.OnLeftClickMouseButton += OnLeftClickMouseButtoned;
            _inputCatchKeyClick.OnLeftClickMouseButtonUp += OnLeftClickMouseButtonUped;
            _inputCatchKeyClick.OnRightClickMouseButtonDown += OnRightClickMouseButtonDowned;
            _inputCatchKeyClick.OnAlpha1KeyDown += OnAlpha1KeyDowned;
            _inputCatchKeyClick.OnAlpha2KeyDown += OnAlpha2KeyDowned;
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
                    unit.IsCommandToMove(hit.point);
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
            _inputCatchKeyClick.OnAlpha1KeyDown -= OnAlpha1KeyDowned;
            _inputCatchKeyClick.OnAlpha2KeyDown -= OnAlpha2KeyDowned;
        }
        
        private void OnAlpha1KeyDowned()
        {
            _spawner.Spawn(0);
        }

        private void OnAlpha2KeyDowned()
        {
            _spawner.Spawn(1);
        }

    }
}

