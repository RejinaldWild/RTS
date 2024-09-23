using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    private Camera _camera;
    private NavMeshAgent _agent;
    public LayerMask Ground;

    void Start()
    {
        _camera = Camera.main;
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, Ground))
            {
                _agent.SetDestination(hit.point);
            }
        }
    }
}
