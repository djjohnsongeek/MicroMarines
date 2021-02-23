using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// Class based off of https://github.com/DoogeJ/MonoGame.Primitives2D
namespace Micro_Marine.src
{
    class Line
    {
        private Vector2 point1;
        private Vector2 point2;
        private SpriteBatch spriteBatch;
        private Texture2D pixel;
        private Color color;

        public Line(SpriteBatch sBatch, Vector2 startPoint, Vector2 endPoint, Color color)
        {
            spriteBatch = sBatch;
            point1 = startPoint;
            point2 = endPoint;
            this.color = color;
            createPixel(spriteBatch);
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            float distance = Vector2.Distance(point1, point2);
            float thickness = 1.0f;

            spriteBatch.Draw(
                 pixel,
                 point1,
                 null,
                 color,
                 angle,
                 Vector2.Zero,
                 new Vector2(distance, thickness),
                 SpriteEffects.None,
                 0);
        }

        private void createPixel(SpriteBatch spriteBatch)
        {
            pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White });
        }
    }
}
