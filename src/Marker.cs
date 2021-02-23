using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Micro_Marine.src
{
    class Marker
    {
        private Vector2 position;
        private Texture2D texture;

        public Marker(ContentManager Content, Vector2 position)
        {
            texture = Content.Load<Texture2D>("red_marker");
            this.position = position;
        }

        public void Draw(SpriteBatch sBatch)
        {
            sBatch.Draw(texture, position, Color.Red);
        }
    }
}
