using _RTSGameProject.Logic.Common.Selection;
using _RTSGameProject.Logic.StateMachineAI.Core;
using _RTSGameProject.Logic.StateMachineAI.Implementation;
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
        private InputManager _inputManager;
        private StateMachineAi _stateMachineAi;
        private bool _isMove;
        private bool _isAttack;
        private bool _isIdle;

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
            _stateMachineAi = new(new IState[]
            {
                new UnitMoveState(), new UnitIdle()},//, new UnitAttack()
                new Transition[]
            {
                new Transition(typeof(UnitIdle),typeof(UnitMoveState),()=>_isMove),
                new Transition(typeof(UnitMoveState),typeof(UnitIdle),()=>_isIdle),
                // new Transition(typeof(UnitIdle),typeof(UnitAttack),()=>_isAttack),
                // new Transition(typeof(UnitAttack),typeof(UnitIdle),()=>_isAttack),
                // new Transition(typeof(UnitAttack),typeof(UnitMovement),()=>_isMove),
                // new Transition(typeof(UnitMovement),typeof(UnitAttack),()=>_isAttack)
            });

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
                if (UnityEngine.Input.GetKey(KeyCode.LeftShift))
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
                    _stateMachineAi.Update();
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

