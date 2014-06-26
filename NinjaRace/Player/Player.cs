using VitPro;
using VitPro.Engine;
using System;

class Player : IUpdateable, IRenderable
{
    public Vec2 Position, Velocity, Size;
    public PlayerState State;
    public IController Controller;
    public CollisionBox Box { get { return new CollisionBox(Position, Size); } }
    public Player(IController controller)
    {
        Size = new Vec2(10, 20);
        State = new PlayerState(this);
        Controller = controller;
    }
    public void Update(double dt)
    {
        State.Update(dt);
        Position += Velocity * dt;
    }

    public void Render()
    {
        State.Render();
    }
}