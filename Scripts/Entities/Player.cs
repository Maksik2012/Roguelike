using System;
using Godot;
using Roguelike.Scripts.Utilities;

namespace Roguelike.Entities
{
    public class Player : Entity
    {
        private Camera2D _Camera;
        
        public Player() 
        {
            Data.Sprite.Texture = ImageLoader.LoadTexture("res://Sprites/Entities/Player/Knight.png", true);
            Data.InitBody(4, 6, new Vector2(0, -16));
            Data.InitCollider(3.5f, 3);

            _Camera = new Camera2D();
            _Camera.Current = true;
         // _Camera.Zoom = new Vector2(5, 5);

            AddChild(_Camera);

            Body.PhysicsProccess += Control;
        }

        private void Control(float delta) 
        {
            GetInputDirection();
        }

        private void GetInputDirection()
        {
            Data.Velocity.x = Godot.Input.GetActionStrength("Right") - Godot.Input.GetActionStrength("Left");
            Data.Velocity.y = Godot.Input.GetActionStrength("Down") - Godot.Input.GetActionStrength("Up");
        }
        public void SetPosition(Vector2 pos)
        {
            Body.GlobalPosition = pos;
        }
    }
}
