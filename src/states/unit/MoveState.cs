using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Micro_Marine.src.states.unit
{
    class MoveState : BaseState
    {
        public MoveState ()
        {
            Name = "MoveState";
        }

        public override void Enter(Unit unit)
        {
            base.Enter(unit);
            unit.CurrAnimType = AnimationType.Walking;
            unit.Animation.SetInterval(0.05f);
        }

        public override void Update(float dt)
        {
            base.Update(dt);

            if (unit.ReceivesMoveCommand())
            {
                unit.OverWriteWaypoints();
            }

            if (unit.ReceivesQueueCommand())
            {
                unit.Waypoints.Enqueue(Input.GetMouseWorldPos());
                unit.ReadyForCommand = false;
            }

            unit.GetNextWaypoint();
            unit.FollowWaypoints(dt);
        }
    }
}
