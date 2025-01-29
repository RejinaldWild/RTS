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
                    new UnitAttack(unit),
                    new MoveToEnemy(unit, UnitsRepository)
                },
                new Transition[]
                {
                    new(typeof(UnitIdle), typeof(MoveToEnemy), ()=> !unit.IsCommandedToMove && UnitsRepository.HasEnemy(unit) & unit.HasEnemy && unit.IsCloseToMove && !unit.IsCloseToAttack),
                    new(typeof(UnitIdle), typeof(UnitPatrolling), () => !unit.HasEnemy && unit.Team==1),
                    new(typeof(MoveToEnemy), typeof(UnitIdle), ()=> unit.Team!=1 & !unit.HasEnemy && !unit.IsCloseToMove),
                    new(typeof(UnitPatrolling), typeof(MoveToEnemy), () => unit.Team==1 && UnitsRepository.HasEnemy(unit) && !unit.HasEnemy && unit.IsCloseToMove),
                    new (typeof(MoveToEnemy), typeof(UnitAttack), () => !unit.IsCommandedToMove && !unit.IsCommandedToAttack && unit.HasEnemy && unit.IsCloseToAttack && !unit.InAttackCooldown),
                    new (typeof(UnitAttack), typeof(MoveToEnemy), () => !unit.IsCommandedToMove && UnitsRepository.HasEnemy(unit) && unit.HasEnemy && unit.IsCloseToMove && !unit.IsCloseToAttack && !unit.InAttackCooldown),
                    
                });
        }
    }
}
