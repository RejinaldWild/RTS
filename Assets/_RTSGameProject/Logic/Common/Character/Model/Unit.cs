using UnityEngine;

namespace _RTSGameProject.Logic.Common.Character.Model
{
    public class Unit : MonoBehaviour
    {
        public Vector3 Position { get; set; }
        [field: SerializeField] public Vector3 StartPosition{get; private set;}
        [field: SerializeField] public int Team { get; set; }
        [field: SerializeField] public int CurrentPositionIndex { get; set; }
        
        private UnitMovement _unitMovement;
        private PatrollMovement _patrollMovement;

        private void Start()
        {
            StartPosition = transform.position;
            Position = StartPosition;
            _unitMovement = GetComponent<UnitMovement>();
            _patrollMovement = GetComponent<PatrollMovement>();
        }
    
        public void Move()
        {
            _unitMovement.Move(Position);
            if ((Position - transform.position).magnitude<0.5f)
            {
                StartPosition = Position;
            }
        }

        public void Patrolling()
        {
            _patrollMovement.Move(this, _unitMovement);
        }
    }
}