using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Micro_Marine.src
{
    class UnitManager // Handles unit selection, deletion
    {
        private Dictionary<ushort, Unit> selectedUnits;
        private List<Unit> units;
        private SelectBox selectBox;
        private SpriteBatch spriteBatch;

        // TODO add units to selected units dict
        // use selected units to draw waypoints

        public UnitManager(List<Unit> units, SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            selectedUnits = new Dictionary<ushort, Unit>();
            this.units = units;
            selectBox = new SelectBox(spriteBatch.GraphicsDevice);
        }

        public void Update(GameTime gameTime)
        {
            selectBox.Update();
            foreach (Unit unit in units)
            {
                unit.Update(gameTime);
                selectBox.CheckSelection(unit);
            }
            selectBox.ResetSelection();
        }

        public void Draw()
        {
            foreach (Unit unit in units)
            {
                unit.Draw(spriteBatch);
            }

            selectBox.Draw(spriteBatch);
        }


        private void selectUnit(ushort id)
        {
            Unit unit = units[id];
            selectedUnits.Add(id, unit);
        }

        private void deselectUnit(ushort id)
        {
            selectedUnits.Remove(id);
        }

        private void deleteUnit(ushort id)
        {
            if (selectedUnits.ContainsKey(id))
            {
                selectedUnits.Remove(id);
            }
        }
    }
}
