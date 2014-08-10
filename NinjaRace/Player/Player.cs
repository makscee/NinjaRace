using VitPro;
using VitPro.Engine;
using System;
using System.Collections.Generic;

partial class Player : IUpdateable, IRenderable
{
    public Vec2 Position, Velocity, Size;
    public PlayerState State;
    public double Speed = 250, Acc = 2700, Gravity = 700, GAcc = 1600, JumpForce = 400, DropSpeed = 50, DropAcc = 150;
    public IController Controller;
    public CollisionBox Box { get { return new CollisionBox(Position, Size); } }
    public Dictionary<Side, List<Tile>> collisions = new Dictionary<Side,List<Tile>>();

    public Player(IController controller)
    {
        Size = new Vec2(10, 20);
        State = new Walking(this);
        Controller = controller;
        collisions.Add(Side.Left, new List<Tile>());
        collisions.Add(Side.Right, new List<Tile>());
        collisions.Add(Side.Up, new List<Tile>());
        collisions.Add(Side.Down, new List<Tile>());
    }
    public void Update(double dt)
    {
        if (Controller.NeedJump())
            State.Jump();
        State.Update(dt);
        Position += Velocity * dt;
    }

    public void StateChangeCheck()
    {
        if (collisions[Side.Left].Count == 0 &&
           collisions[Side.Right].Count == 0 &&
           collisions[Side.Down].Count == 0)
        {

            State = (State is Flying)? State : new Flying(this);
            return;
        }
        if (collisions[Side.Down].Count != 0)
        {
            State = (State is Walking) ? State : new Walking(this);
            return;
        }
        if (collisions[Side.Down].Count == 0)
        {
            if (State is WallGrab)
                return;
            if (collisions[Side.Left].Count > 0 && Velocity.X <= 0)
                State = new WallGrab(this, Side.Left);
            if (collisions[Side.Right].Count > 0 && Velocity.X >= 0)
                State = new WallGrab(this, Side.Right);
            return;
        }
        

    }

    public void Render()
    {
        State.Render();
    }
}