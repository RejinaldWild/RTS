using System;
using System.Collections.Generic;
using System.Threading;
using _RTSGameProject.Logic.Common.Services;
using Cysharp.Threading.Tasks;
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
        [field: SerializeField] public Vector3 Position { get; set; }
        
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
        private CancellationTokenSource _cancellationTokenSource;
        
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
            _cancellationTokenSource = new CancellationTokenSource();
        }
    
        public void OnPaused()
        {
            _cancellationTokenSource?.Cancel();
            _unitMovement.Stop();
            _attackAct.Stop();
        }
        
        public void OnUnPaused()
        {
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();
            _unitMovement.Continue();
            _attackAct.Continue();
        }
        
        public void Move()
        {
            _unitMovement.Move(Position);
            EndPathAsync().Forget();
        }
        
        public void MoveTo()
        {
            if(this != null && _enemy!=null)
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
            AttackCommandAsync().Forget();
            Debug.Log("Unit is attacking!");
            EndCommandAsync().Forget();
        }

        public void TakeDamage(float damage)
        {
            Health.TakeDamage(damage);
        }
        
        public void AssignEnemy(Unit enemy)
        {
            if (_enemy != enemy && enemy!=null)
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
            if (_enemy != null && this != null)
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
        
        private async UniTask EndPathAsync()
        {
            while(IsCommandedToMove && !_cancellationTokenSource.IsCancellationRequested)
            {
                IsCommandedToMove = _unitMovement.EndPath(IsCommandedToMove);
                await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
            }
        }
        
        private async UniTask AttackCommandAsync()
        {
            while(this != null && _enemy != null && !_cancellationTokenSource.IsCancellationRequested &&
                  Mathf.Pow(_attackAct.DistanceToAttack, 2f) >= Vector3.SqrMagnitude(transform.position - _enemy.transform.position)) //?
            {
                _attackAct.Execute(_enemy);
                await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            }
            
        }
        
        private async UniTask EndCommandAsync()
        {
            while(IsCommandedToAttack && !_cancellationTokenSource.IsCancellationRequested)
            {
                IsCommandedToAttack = _attackAct.EndCommand(IsCommandedToAttack,_enemy);
                await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
            }
        }

        private void OnDestroy()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }
    }
}