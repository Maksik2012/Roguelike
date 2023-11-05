using Godot;
using System;

namespace Roguelike.Entities
{
    public abstract class Entity
    {
        public EntityBody Body;
        public EntityData Data;

        public Entity() { 
            Data = new EntityData();
            Body = new EntityBody(Data);

            AddChild(Data.Sprite);
            AddChild(Data.Collider);

            Body.PhysicsProccess = PhysicsProccess;
            Body.Proccess = Proccess;
            Body.Input = Input;
        }

        public void PhysicsProccess(float delta)
        {

        }
        public void Proccess(float delta)
        {

        }
        public void Input(InputEvent ev)
        {

        }
        public void ConnectToNode(Node parent) 
        {
            parent.AddChild(Body);
        }
        public void DisconnectFromNode(Node parent) 
        { 
            parent.RemoveChild(Body);
        }
        public void AddChild(Node child)
        {
            Body.AddChild(child);
        }
        public void RemoveChild(Node child)
        {
            Body.RemoveChild(child);
        }
    }
}
