using System;
using UnityEngine;

namespace _RTSGameProject.Logic.Common.Character.Model
{
    internal class UnitAttackAct: MonoBehaviour
    {
        public bool InCooldown =>_currentCooldown > 0.5f;

        [field: SerializeField] public float DistanceToAttack { get;private set; }
        
        [field: SerializeField] private float _currentCooldown;
        
        [SerializeField] private float _damage;
        [SerializeField] private float _cooldown;
        
        private bool _isCloseToEnemy;
        private float _pauseCooldown;
        
        private void Start()
        {
            _currentCooldown = 1.5f;
        }
        
        private void Update()
        {
            _currentCooldown = Mathf.Max(_currentCooldown - Time.deltaTime, 0f);
        }

        public void Execute(Unit enemy)
        {
            float distanceToEnemy = Vector3.SqrMagnitude(transform.position - enemy.transform.position);
            float attackDistance = Mathf.Pow(DistanceToAttack, 2f);
            
            if (distanceToEnemy<=attackDistance && !InCooldown)
            {
                enemy.TakeDamage(_damage);
                _currentCooldown = _cooldown;
            }
            else
            {
                Debug.Log("Unit is in cooldown to attack");
            }
        }
        
        public bool EndCommand(bool priority, Unit enemy)
        {
            if (enemy != null)
            {
                float distanceToEnemy = Vector3.SqrMagnitude(transform.position - enemy.transform.position);
                float attackDistance = Mathf.Pow(DistanceToAttack, 2f);
                
                if (distanceToEnemy<attackDistance || !enemy.IsAlive.Value)
                {
                    return false;
                }
            }

            return priority;
        }

        public void Stop()
        {
            _pauseCooldown = _currentCooldown;
            _currentCooldown = Single.PositiveInfinity;
        }

        public void Continue()
        {
            _currentCooldown = _pauseCooldown;
        }
    }
}