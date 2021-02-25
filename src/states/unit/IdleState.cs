using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Micro_Marine.src.states.unit
{
    class IdleState : BaseState
    {
        public IdleState() {
            Name = "IdleState";
        }

        public override void Enter(Unit unit)
        {
            base.Enter(unit);
            unit.CurrAnimType = AnimationType.Idle;
            unit.Animation.SetInterval(0.15f);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (unit.ReceivesMoveCommand())
            {
                unit.OverWriteWaypoints();
                unit.State.Change("move");
            }

            if (unit.ReceivesQueueCommand())
            {
                unit.Waypoints.Enqueue(Input.GetMouseWorldPos());
                unit.State.Change("move");
            }
        }


        public override void Draw(SpriteBatch sBatch)
        {
            base.Draw(sBatch);
        }

    }
}
