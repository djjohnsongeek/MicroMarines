using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Micro_Marine.src
{
    class Input
    {
        public static MouseState mState;
        public static KeyboardState kState;

        public static MouseState prevMState;
        public static KeyboardState prevKState;

        public static void Update()
        {
            mState = Mouse.GetState();
            kState = Keyboard.GetState();
        }

        public static void UpdatePrev()
        {
            prevKState = kState;
            prevMState = mState;
        }

        public static bool KeyWasReleased(Keys key)
        {
            return prevKState.IsKeyDown(key) && kState.IsKeyUp(key);
        }

        public static bool KeyWasPressed(Keys key)
        {
            return prevKState.IsKeyUp(key) && kState.IsKeyDown(key);
        }

        public static bool KeyIsDown(Keys key)
        {
            return kState.IsKeyDown(key);
        }

        public static bool LeftMouseWasPressed()
        {
            return prevMState.LeftButton == ButtonState.Pressed && mState.LeftButton == ButtonState.Released;
        }

        public static bool RightMouseWasPressed()
        {
            return prevMState.RightButton == ButtonState.Pressed && mState.RightButton == ButtonState.Released;
        }

        public static bool LeftMouseIsPressed()
        {
            return mState.LeftButton == ButtonState.Pressed && prevMState.LeftButton == ButtonState.Pressed;
        }

        public static Vector2 GetMouseWorldPos()
        {
            return Camera.GetWorldLocation(new Vector2(mState.X, mState.Y));
        }

        public static bool MouseCollides(Unit unit)
        {
            Vector2 unitPosition = unit.GetPosition();
            int unitHeight = unit.GetHeight();
            int unitWidth = unit.GetWidth();

            Vector2 mousePos = GetMouseWorldPos();

            return (mousePos.X >= (unitPosition.X - unitWidth / 2)) &&
                   (mousePos.X <= (unitPosition.X + unitWidth / 2)) &&
                   (mousePos.Y >= (unitPosition.Y - unitHeight / 2)) &&
                   (mousePos.Y <= (unitPosition.Y + unitHeight / 2));
        }
    }
}
