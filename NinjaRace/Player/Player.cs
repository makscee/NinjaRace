using VitPro;
using VitPro.Engine;
using System;
using System.Collections.Generic;

partial class Player : IUpdateable, IRenderable
{
    public int Dir = 1;
    public Vec2 Position, Size;
    private Vec2 _Velocity, _StartPosition;
    public Color Color;
    public States States;
    public Action bonus = () => { };
    public int lives = 3;
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

    public Player(Vec2 StartPosition, Color color)
    {
        Color = color;
        _StartPosition = StartPosition;
        Position = StartPosition;
        States = new States(this);
        Size = new Vec2(12, 19);
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
            if(States.current is Walking && nd.Y == 0 || (States.current is Flying || States.current is WallGrab) && nd.Y != 0)
                States.Set(new Dash(this, nd));

        }
        States.Update(dt);
        UpdateSword(dt);
        if (Position.Y < -50)
            States.current.Die(Position - Vec2.OrtY);
        if (Controller.NeedBonus())
            bonus.Apply();
    }

    public void Render()
    {
        States.Render();
    }

    public void Respawn()
    {
        Velocity = Vec2.Zero;
        Position = _StartPosition;
        States.SetFlying();
        CalculateCollisions();
        SpeedUp = 1;
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