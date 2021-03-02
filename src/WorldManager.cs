using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
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
        private UnitManager unitManager;

        // map


        public WorldManager(ContentManager content, SpriteBatch spriteBatch)
        {
            contentManager = content;
            this.spriteBatch = spriteBatch;
            unitManager = new UnitManager(Units, spriteBatch);
        }

        public void Load()
        {
            // add a singular scout
            addUnit();
        }

        public void Update(GameTime gameTime)
        {

            unitManager.Update(gameTime);

            if (Input.KeyWasPressed(Keys.Space))
            {
                addUnit();
            }

            // Util.Print($"{Units.Count}");
        }

        public void Draw()
        {
            unitManager.Draw();
        }

        private void addUnit()
        {
            Units.Add(new Unit(contentManager, "marine-Sheet32", next_id));
            next_id++;
        }
    }
}
