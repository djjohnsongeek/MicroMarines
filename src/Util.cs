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
    }
}