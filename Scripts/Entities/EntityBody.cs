using System;
using Godot;

namespace Roguelike.Entities
{
    public class EntityBody : KinematicBody2D
    {
        private EntityData _Data;

        public Action<float> PhysicsProccess = null;
        public Action<float> Proccess = null;
        public Action<InputEvent> Input = null;

        public EntityBody(EntityData data)
        {
            _Data = data;
        }

        public override void _PhysicsProcess(float delta)
        {
            PhysicsProccess?.Invoke(delta);

            if (_Data.Velocity != Vector2.Zero)
                Move();
            
        }

        public override void _Process(float delta)
        {
            Proccess?.Invoke(delta);   
        }

        public override void _Input(InputEvent ev)
        {
            Input?.Invoke(ev);
        }

        private void Move()
        {
            UpdateLookDirection();
            MoveAndSlide(_Data.Velocity.Normalized() * _Data.Speed);
        }

        private void UpdateLookDirection() 
        {
            if (_Data.Velocity.x > 0)
                _Data.Sprite.FlipH = false;

            else if (_Data.Velocity.x < 0)
                _Data.Sprite.FlipH = true;
            
        }
    }
}
