using Godot;
using System;

namespace Roguelike.Entities
{
    public class EntityData
    {
        public Sprite Sprite { get; private set; }
        public CollisionShape2D Collider { get; private set; }
        public CapsuleShape2D Shape { get; private set; }

        public Vector2 Velocity;
        public float Speed = 130;

        public EntityData()
        {
            Sprite = new Sprite();
            Collider = new CollisionShape2D();
            Shape = new CapsuleShape2D();

            Collider.Shape = Shape;
            Collider.RotationDegrees = 90;
            Collider.Disabled = true;
        }

        public void InitCollider(float radius, float height)
        {
            Shape.Radius = radius;
            Shape.Height = height;
        }

        public void InitBody(int vFrames, int hFrames, Vector2 offsetPos) 
        { 
            Sprite.Vframes = vFrames;
            Sprite.Hframes = hFrames;
            Sprite.Position = offsetPos;
        }

    }
}
