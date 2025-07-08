using _RTSGameProject.Logic.Common.Config;
using _RTSGameProject.Logic.Common.Services;
using UnityEngine;
using UnityEngine.AI;

namespace _RTSGameProject.Logic.Common.Character.Model
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class UnitMovement : MonoBehaviour
    {
        private const float MAX_THRESHOLD = 0.3f;
        
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private float _updatePathCooldown;
        [SerializeField] private float _currentCooldown;
        [SerializeField] private UnitAttackAct _unitAttackAct;
        private bool InCooldown => _currentCooldown >0.1f;

        public void Construct(ParamConfig paramConfig)
        {
            _updatePathCooldown = paramConfig.UpdatePathCooldown;
            _currentCooldown = paramConfig.CurrentPathCooldown;
        }
        
        private void Update()
        {
            _currentCooldown = Mathf.Max(_currentCooldown - Time.deltaTime, 0f);
        }
        
        public bool EndPath(bool priority)
        {
            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                return false;
            }

            return priority;
        }
            
        public void Move(Vector3 point)
        {
            _agent.SetDestination(point);
            _currentCooldown = _updatePathCooldown;
        }

        public void MoveTo(Unit unit, Vector3 unitPosition, int team)
        {
            if (_agent.isOnNavMesh && _agent.isActiveAndEnabled)
            {
                if ((team != unit.Team && Vector3.Distance( _agent.destination,unitPosition) > MAX_THRESHOLD) || !InCooldown)
                {
                    Vector3 direction = unitPosition - _agent.transform.position;
                    Vector3 targetPosition = unitPosition - direction.normalized * _unitAttackAct.DistanceToAttack;
                    _agent.destination = targetPosition; //?
                    _currentCooldown = _updatePathCooldown;
                }
            }
        }

        public void Stop()
        {
            _agent.isStopped = true;
        }

        public void Continue()
        {
            _agent.isStopped = false;
        }
    }
}