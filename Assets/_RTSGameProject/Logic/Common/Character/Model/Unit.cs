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
        
        public bool CloseEnoughToMove => IsCloseEnoughToMove();
        public bool CloseEnoughToAttack => IsCloseEnoughToAttack();
        public bool InAttackCooldown => _attackAct.InCooldown;
        public bool IsMoveCondition => IsMove();
        public bool HasEnemy => _enemy!=null;
        public bool CommandToMove => IsCommandToMove(Position);

        private UnitMovement _unitMovement;
        private PatrollMovement _patrollMovement;
        private UnitAttackAct _attackAct;

        public void Construct(int teamId)
        {
            Team = teamId;
            Health = GetComponent<Health>();
        }
        
        private void Start()
        {
            Position = transform.position;
            _unitMovement = GetComponent<UnitMovement>(); // Start works after StateMachine -> _unitMovement,_patrollMovement, _attackAct == null
            _patrollMovement = new PatrollMovement();
            _attackAct = GetComponent<UnitAttackAct>();
            Health = GetComponent<Health>();
        }
    
        public void Move()
        {
            _unitMovement.Move(Position); // NullReferenceException for unit
        }

        public bool IsMove()
        {
            if ((Position - transform.position).magnitude < MAX_THRESHOLD)
            {
                Position = transform.position;
                return false;
            }
            
            return true;
        }
        
        public bool IsCommandToMove(Vector3 target)
        {
            if (target != transform.position)
            {
                return true;
            }

            return false;
        }
        
        public void MoveTo()
        {
            _unitMovement.MoveTo(_enemy, _enemy.Position, Team); // enemy position is not enemy position - it is unit position
        }
        
        public void Patrolling()
        {
            _patrollMovement.Move(this, _unitMovement); // NullReferenceException for enemy
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

        private bool IsCloseEnoughToMove()
        {
            float distanceDiff = Vector3.SqrMagnitude(Position - _enemy.Position); //? enemy position is not enemy position - it is unit position
            float dis = DistanceToFindEnemy;
            return distanceDiff <= dis;
        }
        
        private bool IsCloseEnoughToAttack()
        {
            float distanceDiff = Vector3.SqrMagnitude(Position - _enemy.Position); //? enemy position is not enemy position - it is unit position
            float dis = Mathf.Pow(_attackAct.Distance, 2f);
            return distanceDiff <= dis;
        }
    }
}