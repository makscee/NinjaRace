using VitPro;
using VitPro.Engine;
using System;
using System.Collections.Generic;

partial class Player : IUpdateable, IRenderable
{
    public int Dir = 1, Lives = 3;
    public Vec2 Position, Size, StartPosition;
    private Vec2 _Velocity;

    public Color Color;
    public States States;
    public Action Bonus = () => { };
    public double Speed = 280,
        Acc = 2700,
        Gravity = 1200,
        GAcc = 1800,
        JumpForce = 365,
        SlideSpeed = 50,
        SlideAcc = 1200,
        SpeedUp = 1;
    public bool DoRender = true;
    public IController Controller;
    public CollisionBox Box { get { return new CollisionBox(Position, Size); } }
    public Dictionary<Side, List<Tile>> collisions = new Dictionary<Side,List<Tile>>();
    public Action Respawn, NextDeath = () => { };

    public Vec2 Velocity
    {
        get { return _Velocity; }
        set
        {
            _Velocity = value;
        }
    }

    public Player(Vec2 startPosition, Color color)
    {
        Color = color;
        StartPosition = startPosition;
        States = new States(this);
        Size = new Vec2(12, 19);
        collisions.Add(Side.Left, new List<Tile>());
        collisions.Add(Side.Right, new List<Tile>());
        collisions.Add(Side.Up, new List<Tile>());
        collisions.Add(Side.Down, new List<Tile>());
        Respawn = () =>
        {
            Velocity = Vec2.Zero;
            Position = this.StartPosition;
            States.SetFalling();
            CalculateCollisions();
        };
        Velocity = Vec2.Zero;
        Position = this.StartPosition;
        States.SetWalking();
    }

    public void Reset(Vec2 startPosition)
    {
        Velocity = Vec2.Zero;
        StartPosition = startPosition;
        Position = StartPosition;
        States.SetWalking();
        Respawn = () =>
        {
            Velocity = Vec2.Zero;
            Position = this.StartPosition;
            States.SetFalling();
            CalculateCollisions();
        };
        SpeedUp = 1;
    }

    public Player SetControls(IController controller)
    {
        Controller = controller;
        return this;
    }

    public void Update(double dt)
    {
        CollisionHits();
        if (!States.IsDead)
        {
            Dir = Controller.NeedVel().X > 0 ? 1 : Dir;
            Dir = Controller.NeedVel().X < 0 ? -1 : Dir;
        }
        Vec2 nd = Controller.NeedDash();
        if (!nd.Equals(Vec2.Zero))
        {
            if(States.IsWalking && nd.Y == 0 || (States.IsFlying || States.IsWallgrab) && nd.Y != 0)
                States.Set(new Dash(this, nd));

        }
        States.Update(dt);
        UpdateSword(dt);
        if (Controller.NeedBonus() && !States.IsDead)
            Bonus.Apply();
    }

    public void Render()
    {
        if(DoRender)
            States.Render();
    }

    public void ChangeSpawn()
    {
        List<StartTile> l = Program.World.Level.Tiles.GetStartTiles();
        Vec2 t;
        do
        {
            t = l[Program.Random.Next(l.Count)].Position + Vec2.OrtY * 4;
        }
        while (t.Equals(StartPosition));
        StartPosition = t;
    }

    public void RenderTex(Texture tex)
    {
        RenderState.Push();
        RenderState.Color = Color;
        RenderState.Translate(Position - Size);
        RenderState.Scale(Size * 2);
        if (Dir == -1)
            RenderState.SetOrts(-Vec2.OrtX, Vec2.OrtY, new Vec2(1, 0));
        tex.Render();
        RenderState.Pop();
    }
}