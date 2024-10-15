using System;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    public event Action<Unit> OnUnitAwaked; 
    
    public UnitMovement UnitMovement { get; private set; }

    public NavMeshAgent Agent { get; private set; }

    public FormationController formationController;

    public Vector3 position;
    public void Instantiate()
    {
        OnUnitAwaked?.Invoke(this);
        Agent = GetComponent<NavMeshAgent>();
        UnitMovement = GetComponent<UnitMovement>();
        position = transform.position;
    }
    
    private void Start()
    {
        Instantiate();
    }

    private void Update()
    {
    }

    private void OnDestroy()
    {
        
    }
}
