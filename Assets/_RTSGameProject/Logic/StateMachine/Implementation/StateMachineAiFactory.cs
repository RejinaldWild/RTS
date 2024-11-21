using _RTSGameProject.Logic.Common.Character.Model;
using _RTSGameProject.Logic.StateMachineAI.Core;

namespace _RTSGameProject.Logic.StateMachineAI.Implementation
{
    public class StateMachineAiFactory
    {
        public StateMachine Create(Unit unit)
        {
           return new StateMachine(new UnitIdle(),
                new IState[]
                {
                    new UnitIdle(), new UnitMoveState(unit), new UnitPatrolling(unit), new UnitAttack(unit)

                },
                new Transition[]
                {
                    new Transition(typeof(UnitIdle), typeof(UnitMoveState), () => unit.Position!=unit.StartPosition),
                    new Transition(typeof(UnitMoveState), typeof(UnitIdle), () => unit.Position==unit.StartPosition),
                    new Transition(typeof(UnitIdle), typeof(UnitPatrolling), () => unit.Team==1),
                    //new Transition(typeof(UnitIdle), typeof(UnitAttack), () => unit.Team==1 && unit.DistanceToAttack),
                });
        }
    }
}
