using VitPro;
using VitPro.Engine;
using System;
using System.Collections.Generic;

partial class Player : IUpdateable, IRenderable
{
    public int Dir = 1;
    public Vec2 Position, Size;
    private Vec2 _Velocity, _StartPosition;
    public States States;
    public Action bonus = () => { };
    public double Speed = 250,
        Acc = 2700,
        Gravity = 700,
        GAcc = 1200,
        JumpForce = 350,
        SlideSpeed = 50,
        SlideAcc = 1200,
        SpeedUp = 1;
    public IController Controller;
    public CollisionBox Box { get { return new CollisionBox(Position, Size); } }
    public Dictionary<Side, List<Tile>> collisions = new Dictionary<Side,List<Tile>>();

    public Vec2 Velocity
    {
        get { return _Velocity; }
        set
        {
            _Velocity = value;
        }
    }

    public Player(Vec2 StartPosition)
    {
        _StartPosition = StartPosition;
        Position = StartPosition;
        States = new States(this);
        Size = new Vec2(10, 16);
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
        Vec2 nd = Controller.NeedDash();
        if (!nd.Equals(Vec2.Zero))
        {
            States.Set(new Dash(this, nd));
        }
        States.Update(dt);
        UpdateSword(dt);
        if (Position.Y < -50)
            States.SetDead();
        if (Controller.NeedBonus())
            bonus.Apply();
    }

    public void Render()
    {
        States.Render();
        RenderSword();
    }

    public void Respawn()
    {
        Velocity = Vec2.Zero;
        Position = _StartPosition;
        States.SetFlying();
        CalculateCollisions();
        SpeedUp = 1;
    }
}