using System.Collections.Generic;
using _RTSGameProject.Logic.Common.Character.Model;
using _RTSGameProject.Logic.StateMachineAI.Core;
using UnityEngine;

namespace _RTSGameProject.Logic.Common.Enemy
{
    public class Enemy : MonoBehaviour, IState
    {
        [SerializeField] private UnitMovement _unitMovement;
    
        [field: SerializeField] public List<GameObject> Positions { get; private set; }
        [field: SerializeField] public int CurrentPositionIndex { get; set; }

        private void Start()
        {
            _unitMovement = GetComponent<UnitMovement>();
        }

        public void Move(Vector3 point)
        {
            _unitMovement.Move(point);
        }
    }
}
