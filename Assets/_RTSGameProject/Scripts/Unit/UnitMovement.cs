using UnityEngine;
using UnityEngine.AI;

namespace RTS.Scripts
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class UnitMovement : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;

        public void Move(Vector3 point)
        {
            _agent.SetDestination(point);
        }
    }
}