using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Micro_Marine.src
{
    class Util
    {
        public static void Print(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public static float GetHypotenuse(Vector2 diff)
        {
            return (float)Math.Sqrt((diff.X * diff.X) + (diff.Y * diff.Y));
        }
        
        public static bool MouseCollides(Unit unit)
        {
            Vector2 unitPosition = unit.GetPosition();
            int unitHeight = unit.GetHeight();
            int unitWidth = unit.GetWidth();

            Vector2 mousePos = Input.GetMouseWorldPos();

            return (mousePos.X >= (unitPosition.X - unitWidth / 2)) &&
                   (mousePos.X <= (unitPosition.X + unitWidth / 2)) &&
                   (mousePos.Y >= (unitPosition.Y - unitHeight / 2)) &&
                   (mousePos.Y <= (unitPosition.Y + unitHeight / 2));
        }
    }
}