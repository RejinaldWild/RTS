using _RTSGameProject.Logic.Common.Character.Model;
using _RTSGameProject.Logic.StateMachine.Core;
using UnityEngine;

namespace _RTSGameProject.Logic.StateMachine.Implementation
{
    public class UnitMoveState: IUpdateState
    {
        private Unit _unit;
        private Vector3 _target;

        public UnitMoveState(Unit unit)
        {
            _unit = unit;
        }
        
        public void Update()
        {
            _unit.Move();
        }
    }
}
