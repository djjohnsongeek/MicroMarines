using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Micro_Marine.src
{
    class UnitManager // Handles unit selection, deletion
    {
        private Dictionary<ushort, Unit> selectedUnits;
        private List<Unit> units;

        public UnitManager(List<Unit> units)
        {
            selectedUnits = new Dictionary<ushort, Unit>();
            this.units = units;
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
            if (selectedUnits.ContainsKey(id))
            {
                selectedUnits.Remove(id);
            }
        }
    }
}
