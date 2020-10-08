 using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tutorial_Walking.Tiles
{

    public class Map
    {
        private List<CollisionTiles> collisionTiles = new List<CollisionTiles>();

        public  List<CollisionTiles> CollisionTiles
        {
            get { return collisionTiles; }
        }

        private int width, heigth;

        public int Width
        {
            get { return width; }
        }

        public int Heigth
        {
            get { return heigth; }
        }

        static Texture2D texture;


        public Map()
        {

        }

        public void Generate(int [,] map, int size)
        {
            for (int x = 0; x < map.GetLength(1); x++)
                for (int y = 0; y < map.GetLength(0); y++)
                {
                    int number = map[y, x];

                    if (number > 0)
                        collisionTiles.Add(new CollisionTiles(number, new Rectangle(x * size, y * size, size, size)));

                    width = (x + 1) * size;
                    heigth = (y + 1) * size;
                }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (CollisionTiles tile in collisionTiles)
                tile.Draw(spriteBatch);

        }
    }
}

