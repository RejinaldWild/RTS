using System.Collections.Generic;
using _RTSGameProject.Logic.Common.Services;
using UnityEngine;

namespace _RTSGameProject.Logic.Common.Character.Model
{
    public class Unit : MonoBehaviour
    {
        private const float MAX_THRESHOLD = 0.3f;
        
        [field: SerializeField] public Vector3 StartPosition{get; private set;}
        [field: SerializeField] public int Team { get; set; }
        [field: SerializeField] public int CurrentPositionIndex { get; set; }
        [field: SerializeField] public List<GameObject> Positions{ get; private set; }
        [field: SerializeField] public float DistanceToFindEnemy { get;private set; }

        public Vector3 Position { get; set; }
        public Health Health;
        public Unit _enemy;
        
        public bool CloseEnoughToAttack => IsMoveToEnemy();
        //public bool IsCloseEnoughToAttackEnemy => IsCloseEnoughToAttack();
        public bool InAttackCooldown => _attackAct.InCooldown;
        public bool HasEnemy => _enemy!=null;

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
            Position = transform.position;
            Health = GetComponent<Health>();
            _unitMovement = GetComponent<UnitMovement>();
            _attackAct = GetComponent<UnitAttackAct>();
            _unitFindEnemy = new UnitFindEnemy(DistanceToFindEnemy);
            _patrollMovement = new PatrollMovement();
        }
    
        public void Move()
        {
            _unitMovement.Move(Position);
        }
        
        public void MoveTo()
        {
            _unitMovement.MoveTo(_enemy, _enemy.transform.position, Team); // enemy position is not enemy position - it is unit position
        }
        
        public void Patrolling()
        {
            _patrollMovement.Move(this, _unitMovement);
            Debug.Log("Unit is patrolling!");
        }

        public void Attack()
        {
            _attackAct.Execute(_enemy);
            Debug.Log("Unit is attacking!");
        }

        public void TakeDamage(int damage)
        {
            Health.TakeDamage(damage);
        }
        
        public void AssignEnemy(Unit enemy)
        {
            _enemy = enemy;
            if (_enemy != null)
            {
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
            if (_enemy != null)
            {
                float distanceDiff = Vector3.SqrMagnitude(Position - _enemy.Position);
                float dis = Mathf.Pow(_attackAct.Distance, 2f);
                return distanceDiff <= dis;
            }
            
            return false;
        }
        
        // private bool IsCloseEnoughToAttack()
        // {
        //     float distanceDiff = Vector3.SqrMagnitude(Position - _enemy.Position); //? enemy position is not enemy position - it is unit position
        //     float dis = Mathf.Pow(_attackAct.Distance, 2f);
        //     return distanceDiff <= dis;
        // }
    }
}