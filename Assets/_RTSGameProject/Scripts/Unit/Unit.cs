using UnityEngine;

namespace RTS.Scripts
{
    public class Unit : MonoBehaviour
    {
        private UnitMovement _unitMovement;
        public Vector3 Position { get; set; }
    
        private void Start()
        {
            Position = transform.position;
            _unitMovement = GetComponent<UnitMovement>();
        }
    
        public void Move(Vector3 center)
        {
            _unitMovement.Move(center);
        }
    }
}
