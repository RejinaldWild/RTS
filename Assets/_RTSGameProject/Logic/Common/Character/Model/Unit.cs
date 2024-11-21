using System.Collections.Generic;
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

        [field: SerializeField] private Health _health;
        
        private UnitMovement _unitMovement;
        private PatrollMovement _patrollMovement;
        private UnitAttackAct _attackAct;

        private void Start()
        {
            StartPosition = transform.position;
            Position = StartPosition;
            _unitMovement = GetComponent<UnitMovement>();
            _patrollMovement = new PatrollMovement();
            _attackAct = GetComponent<UnitAttackAct>();
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
        }

        public void Attack()
        {
            _attackAct.Execute();
        }

        public void TakeDamage(int damage)
        {
            _health.TakeDamage(damage);
        }
    }
}