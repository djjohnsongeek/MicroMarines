using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Micro_Marine.src
{
    class WorldManager // Handles world objects
    {
        // entities
        private List<Unit> Units = new List<Unit>();
        private List<Marker> Markers = new List<Marker>();
        private ushort next_id = 0;

        // managers
        private ContentManager contentManager;
        private SpriteBatch spriteBatch;

        // map


        public WorldManager(ContentManager content, SpriteBatch spriteBatch)
        {
            contentManager = content;
            this.spriteBatch = spriteBatch;
        }

        public void Load()
        {
            // add a singular scout
            addUnit();
        }

        public void Update(GameTime gameTime)
        {
            foreach (Unit unit in Units)
            {
                unit.Update(gameTime);
            }
        }

        public void Draw()
        {
            // draw map

            // draw units
            foreach (Unit unit in Units)
            {
                unit.Draw(spriteBatch);
            }
        }

        private void addUnit()
        {
            Units.Add(new Unit(contentManager, "marine-Sheet32", next_id));
            next_id++;
        }
    }
}
