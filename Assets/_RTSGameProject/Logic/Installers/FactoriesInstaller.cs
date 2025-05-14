using _RTSGameProject.Logic.Common.Character.Model;
using _RTSGameProject.Logic.Common.Services;
using _RTSGameProject.Logic.Common.View;
using _RTSGameProject.Logic.StateMachine.Core;
using _RTSGameProject.Logic.StateMachine.Implementation;
using UnityEngine;
using Zenject;

namespace _RTSGameProject.Logic.Installers
{
    public class FactoriesInstaller : MonoInstaller
    {
        [SerializeField] private Unit _unit;
        [SerializeField] private HealthView _healthView;
        
        private IState[] _states;
        private UnitIdle _idleState;
        private UnitAttack _attackState;
        private UnitMoveState _moveState;
        private MoveToEnemy _moveToEnemy;
        private UnitPatrolling _patrolling;
        
        public override void InstallBindings()
        {
            BindUnitFactory();
        }

        private void BindUnitFactory()
        {
            _states = new IState[] { _idleState, _attackState,_moveState,_moveToEnemy, _patrolling };
            Container
                .BindFactory<HealthView, HealthBarFactory>()
                .FromComponentInNewPrefab(_healthView)
                .AsSingle();
            Container
                .BindFactory<Unit, UnitsFactory>()
                .FromComponentInNewPrefab(_unit)
                .AsSingle();
            Container
                .BindFactory<StateMachineActor, StateMachineAiFactory>()
                .AsSingle()
                .WithArguments(_idleState, _states);

        }
    }
}