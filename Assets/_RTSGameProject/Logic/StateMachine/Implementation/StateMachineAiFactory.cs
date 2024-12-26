using _RTSGameProject.Logic.Common.AI;
using _RTSGameProject.Logic.Common.Character.Model;
using _RTSGameProject.Logic.Common.Services;
using _RTSGameProject.Logic.StateMachine.Core;
using UnityEngine.iOS;

namespace _RTSGameProject.Logic.StateMachine.Implementation
{
    public class StateMachineAiFactory:AiFactory
    {
        private InputController _inputController;
        public StateMachineAiFactory(UnitsRepository unitsRepository, ActorsRepository actorsRepository, UnitsFactory unitFactory):base(unitsRepository,actorsRepository, unitFactory)
        {
        }
        
        protected override IAiActor CreateAiActor(Unit unit)
        {
           return new Core.StateMachineActor(new UnitIdle(),
                new IState[]
                {
                    new UnitIdle(), 
                    new UnitMoveState(unit), 
                    new UnitPatrolling(unit), 
                    new UnitFindEnemy(unit, UnitsRepository),
                    new UnitAttack(unit),
                    new MoveToEnemy(unit)
                },
                new Transition[]
                {
                    new Transition(typeof(UnitIdle), typeof(UnitFindEnemy), ()=> !unit.HasEnemy && UnitsRepository.HasEnemy(unit)),
                    new Transition(typeof(UnitFindEnemy), typeof(UnitIdle), ()=> unit.HasEnemy && !unit.CloseEnoughToMove),
                    new Transition(typeof(UnitIdle), typeof(MoveToEnemy), ()=> unit.HasEnemy && !unit.CloseEnoughToAttack), //&& UnitsRepository.HasEnemy(unit)?
                    new Transition(typeof(UnitIdle), typeof(UnitPatrolling), () => !unit.HasEnemy && unit.Team==1),
                    new Transition(typeof(UnitIdle), typeof(UnitMoveState), () => unit.CommandToMove && unit.Team==0 && unit.IsMoveCondition), // point
                    new Transition(typeof(UnitMoveState), typeof(UnitIdle), () => unit.Team==0 && !unit.IsMoveCondition), // point = Position+-threshold
                    
                    //new Transition(typeof(UnitIdle), typeof(UnitAttack), () => unit.HasEnemy && unit.CloseEnoughToAttack && unit.InAttackCooldown),
                    //new Transition(typeof(MoveToEnemy), typeof(UnitAttack), () => unit.HasEnemy && unit.CloseEnoughToAttack && unit.InAttackCooldown),
                    //new Transition(typeof(UnitAttack), typeof(UnitMoveState), () => !unit.HasEnemy && unit.IsMoveCondition),
                    //new Transition(typeof(UnitAttack), typeof(UnitIdle), () => !unit.HasEnemy && !unit.IsMoveCondition),
                    
                    
                    // new Transition(typeof(UnitFindEnemy), typeof(MoveToEnemy), () => unit.HasEnemy && !unit.CloseEnoughToMove),
                    // new Transition(typeof(MoveToEnemy), typeof(UnitIdle), () => !unit.HasEnemy && unit.Team==0),
                    // new Transition(typeof(MoveToEnemy), typeof(UnitPatrolling), () => !unit.HasEnemy && unit.Team==1),
                    // new Transition(typeof(MoveToEnemy), typeof(UnitAttack), () => unit.HasEnemy && unit.CloseEnoughToMove && !unit.InAttackCooldown),
                    // new Transition(typeof(MoveToEnemy), typeof(UnitFindEnemy), () => !unit.HasEnemy && UnitsRepository.HasEnemy(unit)),
                    // new Transition(typeof(UnitAttack), typeof(UnitFindEnemy), () => !unit.HasEnemy && UnitsRepository.HasEnemy(unit)),
                    // new Transition(typeof(UnitAttack), typeof(UnitIdle), () => unit.HasEnemy && unit.CloseEnoughToMove && unit.InAttackCooldown),
                    // new Transition(typeof(UnitPatrolling), typeof(UnitFindEnemy), () => !unit.HasEnemy && UnitsRepository.HasEnemy(unit)),
                });
        }
    }
}
