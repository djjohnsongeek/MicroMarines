﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Micro_Marine.src
{
    class UnitManager
    {
        private Queue<Vector2> waypoints;
        private Dictionary<ushort, Unit> selectedUnits;
        private Dictionary<ushort, Unit> units;
        private ushort id_counter;
        private ContentManager content;

        public UnitManager(ContentManager content)
        {
            units = new Dictionary<ushort, Unit>();

            // initial scout
            addUnit(units, "marine-Sheet32");

            selectedUnits = new Dictionary<ushort, Unit>();
            waypoints = new Queue<Vector2>();
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


        private void addUnit(Dictionary<ushort, Unit> units, string spriteSheet)
        {
            units.Add(id_counter, new Unit(content, spriteSheet, id_counter));
            incrementIdCounter();
        }

        private void selectUnit(ushort id)
        {
            Unit unit = units[id];
            selectedUnits.Add(id, unit);
            units.Remove(id);
        }

        private void deselectUnit(ushort id)
        {
            Unit unit = selectedUnits[id];
            units.Add(id, unit);
            selectedUnits.Remove(id);
        }

        private void deleteUnit(ushort id)
        {
            units.Remove(id);
            selectedUnits.Remove(id);
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