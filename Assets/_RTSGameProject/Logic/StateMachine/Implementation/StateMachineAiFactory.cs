using _RTSGameProject.Logic.Common.AI;
using _RTSGameProject.Logic.Common.Character.Model;
using _RTSGameProject.Logic.Common.Services;
using _RTSGameProject.Logic.StateMachine.Core;

namespace _RTSGameProject.Logic.StateMachine.Implementation
{
    public class StateMachineAiFactory:AiFactory
    {
        private InputController _inputController;
        public StateMachineAiFactory(UnitsRepository unitsRepository, ActorsRepository actorsRepository, UnitsFactory unitFactory, PauseGame pauseGame):base(unitsRepository,actorsRepository, unitFactory, pauseGame)
        {
        }
        
        protected override IAiActor CreateAiActor(Unit unit, PauseGame pauseGame)
        {
           return new StateMachineActor(new UnitIdle(), pauseGame,
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
                    new (typeof(UnitAttack), typeof(UnitIdle), () => pauseGame.OnPaused),
                    new (typeof(MoveToEnemy), typeof(UnitIdle), () => pauseGame.OnPaused),
                    new (typeof(UnitPatrolling), typeof(UnitIdle), () => pauseGame.OnPaused),
                    new (typeof(UnitMovement), typeof(UnitIdle), () => pauseGame.OnPaused),
                    new(typeof(UnitIdle), typeof(MoveToEnemy), ()=> !unit.IsCommandedToMove && UnitsRepository.HasEnemy(unit) & unit.HasEnemy && unit.IsCloseToMove && !unit.IsCloseToAttack),
                    new(typeof(UnitIdle), typeof(UnitPatrolling), () => !unit.HasEnemy && unit.Team==1),
                    new(typeof(MoveToEnemy), typeof(UnitIdle), ()=> !unit.IsCommandedToMove && unit.Team!=1 & !unit.HasEnemy && !unit.IsCloseToMove),
                    new(typeof(UnitPatrolling), typeof(MoveToEnemy), () => unit.Team==1 && UnitsRepository.HasEnemy(unit) && !unit.HasEnemy && unit.IsCloseToMove),
                    new (typeof(MoveToEnemy), typeof(UnitAttack), () => !unit.IsCommandedToMove && !unit.IsCommandedToAttack && unit.HasEnemy && unit.IsCloseToAttack && !unit.InAttackCooldown),
                    new (typeof(UnitAttack), typeof(MoveToEnemy), () => !unit.IsCommandedToMove && UnitsRepository.HasEnemy(unit) && unit.HasEnemy && unit.IsCloseToMove && !unit.IsCloseToAttack && !unit.InAttackCooldown),
                });
        }
    }
}
