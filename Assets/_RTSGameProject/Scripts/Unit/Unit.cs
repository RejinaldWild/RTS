using System;
using UnityEngine;
using UnityEngine.AI;

namespace RTS.Scripts
{
    public class Unit : MonoBehaviour
    {
        public event Action<Unit> OnUnitAwaked;

        public Vector3 Position { get; set; }
        public NavMeshAgent Agent { get; private set; }
        public UnitMovement UnitMovement { get; private set; }
    
        private void Start()
        {
            Instantiate();
        }
    
        public void Instantiate()
        {
            OnUnitAwaked?.Invoke(this);
            Agent = GetComponent<NavMeshAgent>();
            UnitMovement = GetComponent<UnitMovement>();
            Position = transform.position;
        }
    }
}
