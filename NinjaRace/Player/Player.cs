using VitPro;
using VitPro.Engine;
using System;

class Player : IUpdateable, IRenderable
{
    public Vec2 Position, Velocity, Size;
    public PlayerState State;
    public IController Controller;
    public CollisionBox Box { get { return new CollisionBox(Position, Size); } }
    public double JumpForce = 400;

    public Player(IController controller)
    {
        Size = new Vec2(10, 20);
        State = new Walking(this);
        Controller = controller;
    }
    public void Update(double dt)
    {
        if (Controller.NeedJump())
            State.Jump();
        State.Update(dt);
        Position += Velocity * dt;
    }

    public void Render()
    {
        State.Render();
    }
}