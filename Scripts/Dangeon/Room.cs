using Godot;
using System;
using System.Collections.Generic;

namespace Roguelike.Dangeon
{
    public class Room
    {
        public Vector2 Position { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Vector2 Center { get; private set; }

        private Random _Rand = new Random();

        public Room(TileMap tilemap, Vector2 chunkPos, int chunkSize, int minSize, int maxSize)
        {
            Width = _Rand.Next(minSize, maxSize);
            Height = _Rand.Next(minSize, maxSize);
            Position = new Vector2(_Rand.Next((int)chunkPos.x + 2, (int)chunkPos.x + chunkSize - 2 - Width), 
                _Rand.Next((int)chunkPos.y + 2, (int)chunkPos.y + chunkSize - 2 - Height));
            Center = new Vector2(Position.x * 16 + Width * 16 / 2, Position.y * 16 + Height * 16 / 2);

            Vector2 tilePos;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    tilePos.x = Position.x + x;
                    tilePos.y = Position.y + y;
                    tilemap.SetCellv(tilePos, -1);
                }
            }

            tilemap.UpdateBitmaskRegion();
        }
    }
}
