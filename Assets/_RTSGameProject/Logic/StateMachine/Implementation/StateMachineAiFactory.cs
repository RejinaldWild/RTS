using _RTSGameProject.Logic.Common.Character.Model;
using _RTSGameProject.Logic.Common.Services;
using _RTSGameProject.Logic.StateMachine.Core;
using UnityEngine.iOS;

namespace _RTSGameProject.Logic.StateMachine.Implementation
{
    public class StateMachineAiFactory
    {
        public Core.StateMachine Create(Unit unit, UnitRepository unitRepository)
        {
           return new Core.StateMachine(new UnitIdle(),
                new IState[]
                {
                    new UnitIdle(), 
                    new UnitMoveState(unit), 
                    new UnitPatrolling(unit), 
                    new UnitFindEnemy(unit, unitRepository),
                    new UnitAttack(unit),
                    new MoveToEnemy(unit)
                },
                new Transition[]
                {
                    new Transition(typeof(UnitIdle), typeof(UnitFindEnemy), ()=> !unit.HasEnemy && unitRepository.HasEnemy(unit)),
                    new Transition(typeof(UnitIdle), typeof(MoveToEnemy), ()=> unit.HasEnemy && !unit.CloseEnoughToAttack),
                    new Transition(typeof(UnitIdle), typeof(UnitPatrolling), () => !unit.HasEnemy && unit.Team==1),
                    new Transition(typeof(UnitIdle), typeof(UnitMoveState), () => unit.Position!=unit.StartPosition && unit.Team==0),
                    new Transition(typeof(UnitIdle), typeof(UnitAttack), () => unit.HasEnemy && unit.CloseEnoughToAttack && unit.InAttackCooldown),
                    new Transition(typeof(UnitFindEnemy), typeof(MoveToEnemy), () => unit.HasEnemy && !unit.CloseEnoughToAttack),
                    new Transition(typeof(MoveToEnemy), typeof(UnitIdle), () => !unit.HasEnemy && unit.Team==0),
                    new Transition(typeof(MoveToEnemy), typeof(UnitPatrolling), () => !unit.HasEnemy && unit.Team==1),
                    new Transition(typeof(MoveToEnemy), typeof(UnitAttack), () => unit.HasEnemy && unit.CloseEnoughToAttack && !unit.InAttackCooldown),
                    new Transition(typeof(MoveToEnemy), typeof(UnitFindEnemy), () => !unit.HasEnemy && unitRepository.HasEnemy(unit)),
                    new Transition(typeof(UnitAttack), typeof(UnitFindEnemy), () => !unit.HasEnemy && unitRepository.HasEnemy(unit)),
                    new Transition(typeof(UnitAttack), typeof(UnitIdle), () => unit.HasEnemy && unit.CloseEnoughToAttack && unit.InAttackCooldown),
                    new Transition(typeof(UnitPatrolling), typeof(UnitFindEnemy), () => !unit.HasEnemy && unitRepository.HasEnemy(unit)),
                });
        }
    }
}
