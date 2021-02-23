using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Micro_Marine.src.states
{
    class BaseState
    {
        protected Unit unit;
        public string Name = "BaseState";

        public virtual void Enter(Unit unit)
        {
            this.unit = unit;
        }
        public virtual void Exit() { }
        public virtual void Update(float dt)
        {
            unit.UpdateReadyForCommand(dt);
            unit.UpdateSelection();
            unit.Animation.Update(dt);
        }
        public virtual void Draw(SpriteBatch sBatch) 
        {
            Color color = unit.Selected ? Color.Orange : Color.White;
            int frameIndex = (int)unit.CurrOrientation + (int)unit.CurrAnimType;

            sBatch.Draw(
                unit.SpriteSheet,
                unit.Position,
                unit.Frames[frameIndex, unit.Animation.GetFrame()],
                color,
                rotation: 0f,
                origin: new Vector2(unit.GetWidth() / 2, unit.GetHeight() / 2),
                scale: new Vector2(1, 1),
                effects: SpriteEffects.None,
                layerDepth: 0f
            );

            // Util.Print($"frameIndex: {frameIndex}, Frame: {unit.Animation.GetFrame()}");
        }
    }
}
