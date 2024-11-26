using System.Collections.Generic;
using _RTSGameProject.Logic.Common.Services;
using UnityEngine;

namespace _RTSGameProject.Logic.Common.Character.Model
{
    public class Unit : MonoBehaviour
    {
        private const float MAX_THRESHOLD = 0.5f;
        public Vector3 Position { get; set; }
        [field: SerializeField] public Vector3 StartPosition{get; private set;}
        [field: SerializeField] public int Team { get; set; }
        [field: SerializeField] public int CurrentPositionIndex { get; set; }
        [field: SerializeField] public List<GameObject> Positions{ get; private set; }
        [field: SerializeField] public float DistanceToFindEnemy { get;private set; }

        public Health Health;
        
        public bool CloseEnoughToAttack => 
            Vector3.SqrMagnitude(Position - _enemy.Position) <= Mathf.Pow(_attackAct.Distance, 2f);

        public bool InAttackCooldown => _attackAct.InCooldown;
        
        public bool HasEnemy => _enemy!=null;
        private UnitMovement _unitMovement;
        private PatrollMovement _patrollMovement;
        private UnitAttackAct _attackAct;
        public Unit _enemy;


        private void Start()
        {
            StartPosition = transform.position;
            Position = StartPosition;
            _unitMovement = GetComponent<UnitMovement>();
            _patrollMovement = new PatrollMovement();
            _attackAct = GetComponent<UnitAttackAct>();
            Health = GetComponent<Health>();
            DistanceToFindEnemy = 10f;
        }
    
        public void Move()
        {
            _unitMovement.Move(Position);
            if ((Position - transform.position).magnitude<MAX_THRESHOLD)
            {
                StartPosition = Position;
            }
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
            Debug.Log("Enemy has found and assigned");
        }

        public void MoveTo()
        {
            _unitMovement.MoveTo(_enemy.Position);
        }
    }
}