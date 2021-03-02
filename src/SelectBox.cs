using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Micro_Marine.src
{

    class SelectBox
    {
        private Texture2D texture;
        private Rectangle? rect;
        private Rectangle? selectRect;
        private Vector2? startVector;

        public SelectBox(GraphicsDevice graphicsDevice)
        {
            rect = null;
            selectRect = null;
            startVector = null;
            texture = new Texture2D(graphicsDevice, 1, 1);
            texture.SetData(new Color[] { Color.White });
        }

        public void Update()
        {
            // mouse button held down
            if (Input.LeftMouseIsPressed())
            {
                if (startVector == null)
                {
                    startVector = Input.GetMouseWorldPos();
                }
                else
                {
                    Vector2 endVector = Input.GetMouseWorldPos();
                    rect = getRectangle(endVector.X - startVector.Value.X, endVector.Y - startVector.Value.Y);
                }
            }

            // mouse button released
            else if (Input.LeftMouseWasPressed())
            {
                selectRect = rect;
                rect = null;
                startVector = null;
            }
        }

        public void CheckSelection(Unit unit)
        {
            if (selectRect == null) return;

            if (selectRect.Value.Intersects(unit.GetRectangle()))
            {
                unit.Selected = true;
            }
        }

        public void ResetSelection()
        {
            selectRect = null;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (rect != null)
            {
                spriteBatch.Draw(texture, new Rectangle(rect.Value.Left, rect.Value.Top, rect.Value.Width, 1), Color.White);
                spriteBatch.Draw(texture, new Rectangle(rect.Value.Right, rect.Value.Top, 1, rect.Value.Height), Color.White);
                spriteBatch.Draw(texture, new Rectangle(rect.Value.Left, rect.Value.Bottom, rect.Value.Width, 1), Color.White);
                spriteBatch.Draw(texture, new Rectangle(rect.Value.Left, rect.Value.Top, 1, rect.Value.Height), Color.White);
            }
        }

        private Rectangle getRectangle(float width, float height)
        {
            Point rectOrigin = new Point((int)startVector.Value.X, (int)startVector.Value.Y);
            int absWidth = (int)Math.Abs(width);
            int absHeight = (int)Math.Abs(height);

            if (height < 0 && width < 0)
            {
                rectOrigin.X -= absWidth;
                rectOrigin.Y -= absHeight;
            }
            else if (height < 0)
            {
                rectOrigin.Y -= absHeight;
            }
            else if (width < 0)
            {
                rectOrigin.X -= absWidth;
            }

            return new Rectangle(rectOrigin, new Point(absWidth, absHeight));
        }
    }
}
