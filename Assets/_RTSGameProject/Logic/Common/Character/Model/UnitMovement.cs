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

        public void MoveTo(Unit unit, Vector3 unitPosition, int team)
        {
            if (team != unit.Team && _agent.destination != unitPosition)
            {
                _agent.destination = unitPosition;
            }
        }
    }
}