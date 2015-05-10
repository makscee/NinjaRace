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
    public Face Face;

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
        Size = new Vec2(10, 16);
        collisions.Add(Side.Left, new List<Tile>());
        collisions.Add(Side.Right, new List<Tile>());
        collisions.Add(Side.Up, new List<Tile>());
        collisions.Add(Side.Down, new List<Tile>());
        Face = new Face(this);
    }

    public Player SetControls(IController controller)
    {
        Controller = controller;
        return this;
    }

    public void Update(double dt)
    {
        Face.Update(dt);
        CollisionHits();
        Dir = Controller.NeedVel().X > 0 ? 1 : Dir;
        Dir = Controller.NeedVel().X < 0 ? -1 : Dir;
        States.Update(dt);
        if (Position.Y < -50)
            States.SetDead();
    }

    public void Render()
    {
        //States.Render();
        Face.Render();
    }

    public World GetWorld()
    {
        return Program.GetWorld1().player == this ? Program.GetWorld1() : Program.GetWorld2();
    }

    public void Respawn()
    {
        Velocity = Vec2.Zero;
        Position = GetWorld().level.tiles.GetStartTile().Position;
        States.SetFlying();
        CalculateCollisions();
    }
}