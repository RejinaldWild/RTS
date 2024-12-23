using UnityEngine;
using UnityEngine.AI;

namespace _RTSGameProject.Logic.Common.Character.Model
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class UnitMovement : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        
        public void Move(Vector3 point)
        {
            _agent.SetDestination(point);
        }

        public void MoveTo(Vector3 target)
        {
            if(_agent.destination == target)
                return;
            else
            {
                _agent.destination = target;
            }
        }
    }
}