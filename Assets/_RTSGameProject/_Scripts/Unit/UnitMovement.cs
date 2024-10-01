using System;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    public event Action<Vector3> OnUnitChangedPosition;
    
    public LayerMask Ground;
    
    private Camera _camera;
    private NavMeshAgent _agent;
    
    private void Start()
    {
        _camera = Camera.main;
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, Ground))
            {
                OnUnitChangedPosition?.Invoke(hit.point);
                _agent.SetDestination(hit.point);
            }
            
        }
    }
}
