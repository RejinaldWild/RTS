﻿using System;
using UnityEngine;

namespace _RTSGameProject.Logic.Common.Character.Model
{
    internal class UnitAttackAct: MonoBehaviour
    {
        public bool InCooldown => _currentCooldown>0.5f;

        [field: SerializeField] public float Distance { get;private set; }
        
        [field: SerializeField] private float _currentCooldown;
        
        [SerializeField] private int _damage;
        [SerializeField] private float _cooldown;
        
        private bool _isCloseToEnemy;

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
            float distanceToEnemy = Vector3.SqrMagnitude(transform.position - enemy.Position);
            float attackDistance = Mathf.Pow(Distance, 2f);
            
            if (distanceToEnemy<=attackDistance && !InCooldown)
            {
                enemy.TakeDamage(_damage);
                _currentCooldown = _cooldown;
            }
            else
            {
                Debug.Log("Unit is in Cooldown");
            }
        }
        
        public bool EndCommand(bool priority, Unit enemy)
        {
            float distanceToEnemy = Vector3.SqrMagnitude(transform.position - enemy.transform.position);
            float attackDistance = Mathf.Pow(Distance, 2f);
            
            if (distanceToEnemy<attackDistance || !enemy.IsAlive)
            {
                return false;
            }

            return priority;
        }
    }
}