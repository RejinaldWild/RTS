using UnityEngine;
using UnityEngine.AI;

namespace RTS.Scripts
{
    public class UnitMovement : MonoBehaviour
    {
        public LayerMask Ground;
        public FormationController FormationController;
    
        private enum EUnitState
        {
            Move,
            Idle
        }
    
        private Camera _camera;
        private EUnitState CurrentState;
        private RaycastHit hit;
        private Unit unit;
    
        public NavMeshAgent Agent { get; private set; }
    
        private void Start()
        {
            _camera = Camera.main;
            Agent = GetComponent<NavMeshAgent>();
            CurrentState = EUnitState.Idle;
            FormationController.OnUnitChangedPosition += Move;
            unit = GetComponent<Unit>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, Ground))
                {
                    FormationController.SetFormationCenter(hit.point);
                    CurrentState = EUnitState.Move;
                }
            }
        }

        private void FixedUpdate()
        {
            switch (CurrentState)
            {
                case EUnitState.Move:
                    Move(hit.point);
                    break;
            }
        }

        private void OnDestroy()
        {
            FormationController.OnUnitChangedPosition -= Move;
        }

        private void Move(Vector3 point)
        {
            if ((point - Agent.transform.position).magnitude > 1f)
            {
                // TakeSpeed from ScriptableObject
                Agent.SetDestination(hit.point + (unit.Position * 2));
            }
            else
            {
                //Stop movement logic
                ChangeState(EUnitState.Idle);
            }
        }

        private void ChangeState(EUnitState newState)
        {
            CurrentState = newState;
        }
    }
}
