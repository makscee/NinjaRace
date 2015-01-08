using VitPro;
using VitPro.Engine;
using System;
using System.Collections.Generic;

partial class Player : IUpdateable, IRenderable
{
    public int Dir = 1;
    public Vec2 Position, Size;
    private Vec2 _Velocity, _SpeedUp;
    public States States;
    public double Speed = 250, Acc = 2700, Gravity = 700, GAcc = 1200, JumpForce = 400, DropSpeed = 50, DropAcc = 1200, SpeedUpAcc = 100;
    public IController Controller;
    public CollisionBox Box { get { return new CollisionBox(Position, Size); } }
    public Dictionary<Side, List<Tile>> collisions = new Dictionary<Side,List<Tile>>();

    public Vec2 Velocity
    {
        get { return _Velocity + _SpeedUp * SpeedUpAcc; }
        set
        {
            _Velocity = value;
        }
    }
    public Vec2 SpeedUp
    {
        set { _SpeedUp = value; }
    }

    public Player()
    {
        Size = new Vec2(10, 20);
        collisions.Add(Side.Left, new List<Tile>());
        collisions.Add(Side.Right, new List<Tile>());
        collisions.Add(Side.Up, new List<Tile>());
        collisions.Add(Side.Down, new List<Tile>());
    }

    public Player SetControls(IController controller)
    {
        Controller = controller;
        return this;
    }

    public void Update(double dt)
    {
        CollisionHits();
        Dir = Controller.NeedVel().X > 0 ? 1 : Dir;
        Dir = Controller.NeedVel().X < 0 ? -1 : Dir;
        States.Update(dt);
    }

    public void Render()
    {
        States.Render();
        //foreach (var a in collisions)
        //{
        //    foreach (var b in a.Value)
        //    {
        //        Draw.Rect(b.Position + Tile.Size, b.Position - Tile.Size, Color.Magenta);
        //    }
        //}
    }
}