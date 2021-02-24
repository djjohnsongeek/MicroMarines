using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Micro_Marine.src
{
    class UnitManager // Handles unit selection
    {
        private Dictionary<ushort, Unit> selectedUnits;
        private Dictionary<ushort, Unit> units;
        private ushort id_counter;

        public UnitManager(ContentManager content)
        {
            units = new Dictionary<ushort, Unit>();
            selectedUnits = new Dictionary<ushort, Unit>();
        }

        public void Update(float dt)
        {
            // update unit selection

            // update unit actions
        }

        public void Draw()
        {
            // draw units
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
            units.Remove(id);
            if (selectedUnits.ContainsKey(id))
            {
                selectedUnits.Remove(id);
            }
        }

        private void incrementIdCounter()
        {
            if (id_counter == ushort.MaxValue)
            {
                // throw error at some point
            }
            id_counter++;
        }
    }
}
