using UnityEngine;
using UnityEngine.AI;

namespace _RTSGameProject.Logic.Common.Character.Model
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class UnitMovement : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private float _updatePathCooldown;
        [SerializeField] private float _currentCooldown;
        
        private bool InCooldown => _currentCooldown >0.1f;

        private void Update()
        {
            _currentCooldown = Mathf.Max(_currentCooldown - Time.deltaTime, 0f);
        }
            
        public void Move(Vector3 point)
        {
            _agent.SetDestination(point);
            _currentCooldown = _updatePathCooldown;
        }

        public void MoveTo(Unit unit, Vector3 unitPosition, int team)
        {
            if (team != unit.Team && _agent.destination != unitPosition || !InCooldown)
            {
                _agent.destination = unitPosition;
                _currentCooldown = _updatePathCooldown;
            }
        }
    }
}