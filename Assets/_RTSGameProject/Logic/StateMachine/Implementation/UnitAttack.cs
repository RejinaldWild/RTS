﻿using _RTSGameProject.Logic.Common.Character.Model;
using _RTSGameProject.Logic.StateMachine.Core;

namespace _RTSGameProject.Logic.StateMachine.Implementation
{
    public class UnitAttack : IEnterState
    {
        private Unit _unit;

        public UnitAttack(Unit unit)
        {
            _unit = unit;
        }
        public void Enter()
        {
            _unit.Attack();
        }
    }
}