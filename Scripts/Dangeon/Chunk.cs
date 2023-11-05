using System;
using System.Collections.Generic;
using Godot;

namespace Roguelike.Dangeon
{
    public class Chunk
    {
        public Vector2 Position { get; private set; }
        public Room Room { get; set; }

        public Chunk(Vector2 Pos)
        {
            Position = Pos;
        }

    }
}
