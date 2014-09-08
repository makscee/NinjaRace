using VitPro;
using VitPro.Engine;
using System;
using System.Collections.Generic;

partial class Player : IUpdateable, IRenderable
{
    public int Dir = 1;
    public Vec2 Position, Size;
    private Vec2 _Velocity, _SpeedUp;
    public PlayerState State;
    public double Speed = 250, Acc = 2700, Gravity = 700, GAcc = 1200, JumpForce = 400, DropSpeed = 50, DropAcc = 1200, SpeedUpAcc = 100;
    public IController Controller;
    public CollisionBox Box { get { return new CollisionBox(Position, Size); } }
    public Dictionary<Side, List<Tile>> collisions = new Dictionary<Side,List<Tile>>();
    public Ability Ability;

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

    public Player(IController controller)
    {
        Size = new Vec2(10, 20);
        State = new Walking(this);
        Ability = new RocketJump(this);
        Controller = controller;
        collisions.Add(Side.Left, new List<Tile>());
        collisions.Add(Side.Right, new List<Tile>());
        collisions.Add(Side.Up, new List<Tile>());
        collisions.Add(Side.Down, new List<Tile>());
    }
    public void Update(double dt)
    {
        CollisionHits();
        StateChangeCheck();
        if (Controller.NeedJump())
            State.Jump();
        if (Controller.NeedAbility())
            State.AbilityUse();
        Dir = Controller.NeedVel().X > 0 ? 1 : Dir;
        Dir = Controller.NeedVel().X < 0 ? -1 : Dir;
        State.Update(dt);
    }

    public void StateChangeCheck()
    {
        if(State is Dead)
            return;
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
        foreach (var a in collisions)
        {
            foreach (var b in a.Value)
            {
                Draw.Rect(b.Position + Tile.Size, b.Position - Tile.Size, Color.Green);
            }
        }
    }
}