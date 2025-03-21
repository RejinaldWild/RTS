using System;
using System.Collections;
using System.Collections.Generic;
using _RTSGameProject.Logic.Common.Services;
using UniRx;
using UnityEngine;

namespace _RTSGameProject.Logic.Common.Character.Model
{
    public class Unit : MonoBehaviour, ISelectable
    {
        [field: SerializeField] public Vector3 StartPosition{get; private set;}
        [field: SerializeField] public int Team { get; set; }
        [field: SerializeField] public int CurrentPositionIndex { get; set; }
        [field: SerializeField] public List<GameObject> Positions{ get; private set; }
        [field: SerializeField] public float DistanceToFindEnemy { get;private set; }

        public Vector3 Position { get; set; }
        
        public IReadOnlyReactiveProperty<bool> IsAlive => Health.IsAlive;
        
        public Health Health;
        public Unit _enemy;
        public string Id;
        public bool IsCommandedToMove = false;
        public bool IsCommandedToAttack = false;
        public bool IsCloseToMove => IsMoveToEnemy();
        public bool IsCloseToAttack => IsAttackEnemy();
        public bool InAttackCooldown => _attackAct.InCooldown;
        public bool HasEnemy => _enemy!=null && _enemy.IsAlive.Value;

        private UnitMovement _unitMovement;
        private PatrollMovement _patrollMovement;
        private UnitAttackAct _attackAct;
        private UnitFindEnemy _unitFindEnemy;
        private UnitsRepository _unitsRepository;
        
        public void Construct(int teamId, UnitsRepository unitsRepository)
        {
            Team = teamId;
            Health = GetComponent<Health>();
            _unitsRepository = unitsRepository;
        }
        
        private void Awake()
        {
            Id = Guid.NewGuid().ToString();
            Position = transform.position;
            Health = GetComponent<Health>();
            _unitMovement = GetComponent<UnitMovement>();
            _attackAct = GetComponent<UnitAttackAct>();
            _unitFindEnemy = new UnitFindEnemy(DistanceToFindEnemy);
            _patrollMovement = new PatrollMovement();
        }
    
        public void OnPaused()
        {
            StopCoroutine(AttackCommand());
            StopCoroutine(EndCommand());
            StopCoroutine(EndPath());
            _unitMovement.Stop();
            _attackAct.Stop();
        }
        
        public void OnUnPaused()
        {
            _unitMovement.Continue();
            _attackAct.Continue();
        }
        
        public void Move()
        {
            _unitMovement.Move(Position);
            StartCoroutine(EndPath());
        }
        
        public void MoveTo()
        {
            if(this != null && _enemy!=null) //?
            {
                _unitMovement.MoveTo(_enemy, _enemy.transform.position, Team);
            }
        }
        
        public void Patrolling()
        {
            _patrollMovement.Move(this, _unitMovement);
            Debug.Log("Unit is patrolling!");
        }

        public void Attack()
        {
            StartCoroutine(AttackCommand());
            Debug.Log("Unit is attacking!");
            StartCoroutine(EndCommand());
        }

        public void TakeDamage(float damage)
        {
            Health.TakeDamage(damage);
        }
        
        public void AssignEnemy(Unit enemy)
        {
            if (_enemy != enemy && enemy!=null) //?
            {
                _enemy = enemy;
                Debug.Log("Enemy has found and assigned");
            }
        }

        public void RemoveEnemy()
        {
            _enemy = null;
        }
        
        private bool IsMoveToEnemy()
        {
            _unitFindEnemy.FindEnemy(this,_unitsRepository);
            if (_enemy != null && this != null) //?
            {
                float distanceDiff = Vector3.SqrMagnitude(transform.position - _enemy.transform.position); //???
                return distanceDiff<= Mathf.Pow(DistanceToFindEnemy,2f);
            }
            
            return false;
        }
        
        private bool IsAttackEnemy()
        {
            float distanceDiffToAttack = Vector3.SqrMagnitude(transform.position - _enemy.transform.position);
            float distanceToAttack = Mathf.Pow(_attackAct.DistanceToAttack, 2f);
            return distanceToAttack >= distanceDiffToAttack;
        }
        
        private IEnumerator EndPath()
        {
            while(IsCommandedToMove)
            {
                IsCommandedToMove = _unitMovement.EndPath(IsCommandedToMove);
                yield return new WaitForSeconds(0.1f);
            }
        }
        
        private IEnumerator AttackCommand()
        {
            while(this != null && _enemy != null &&
                  Mathf.Pow(_attackAct.DistanceToAttack, 2f) >= Vector3.SqrMagnitude(transform.position - _enemy.transform.position)) //?
            {
                _attackAct.Execute(_enemy);
                yield return new WaitForSeconds(0.5f);
            }
            
        }
        
        private IEnumerator EndCommand()
        {
            while(IsCommandedToAttack)
            {
                IsCommandedToAttack = _attackAct.EndCommand(IsCommandedToAttack,_enemy);
                yield return new WaitForSeconds(0.1f);
            }
        }

    }
}