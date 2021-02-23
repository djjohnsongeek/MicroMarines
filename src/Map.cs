using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Micro_Marine.src
{
    class Map
    {
        Rectangle[] mapTiles;
        int[,] map;
        Texture2D grassSheet;

        public Map(int size, Texture2D texture)
        {
            map = new int[size, size];
            grassSheet = texture;

            parseSheet(64, 0, 64, 64);
            buildMap(size);
        }

        public void DisplayMap(int size)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                }
            }
        }

        private void parseSheet(int xStep, int yStep, int width, int height)
        {
            mapTiles = new Rectangle[7];
            int x = 0; int y = 0;

            for (int i = 0; i < 7; i++)
            {
                mapTiles[i].X = x;
                mapTiles[i].Y = y;
                mapTiles[i].Width = width;
                mapTiles[i].Height = height;

                x += xStep;
                y += yStep;
            }
        }

        private void buildMap(int size)
        {
            Random rand = new Random();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    map[i, i] = rand.Next(0, 7);
                }
            }
        }
    }
}
