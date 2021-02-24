using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Micro_Marine.src.states.unit
{
    class UnitStateMachine
    {
        public BaseState CurrentState;
        private Unit unit;

        public UnitStateMachine(Unit unit)
        {
            CurrentState = null;
            this.unit = unit;
        }

        public void Change(string state)
        {
            BaseState newState = null;
            switch (state)
            {
                case "idle":
                    newState = new IdleState();
                    switchStates(newState);
                    break;
                case "move":
                    newState = new MoveState();
                    switchStates(newState);
                    break;
            }
        }

        public void Update(GameTime gameTime)
        {
            CurrentState.Update(gameTime);
        }

        public void Draw(SpriteBatch sBatch)
        {
            CurrentState.Draw(sBatch);
        }

        private void switchStates(BaseState newState)
        {
            if (CurrentState != null)
            {
                CurrentState.Exit();
            }
            CurrentState = newState;
            CurrentState.Enter(unit);
        }
    }
}
