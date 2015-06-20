using VitPro;
using VitPro.Engine;
using System;

partial class Player
{
    double Delay = 0.5;
    Timer t;
    bool _EnableSword = false;

    public Player EnableSword()
    {
        _EnableSword = true;
        return this;
    }

    public Player GetOpponent()
    {
        return (Program.World.player1 == this) ? Program.World.player2 : Program.World.player1;
    }

    void DoSwing()
    {
        if (new CollisionBox(Position + new Vec2(Size.X * 1.5, 0) * Dir, new Vec2(Size.X * 3, Size.Y / 2))
            .Collide(GetOpponent().Box) != Side.None)
        {
            GetOpponent().States.current.Die(Position);
        }
    }

    void RenderSword()
    {
        if(_EnableSword)
            Draw.Rect(Position + new Vec2(Size.X * 1.5, 0) * Dir + new Vec2(Size.X * 3, Size.Y / 2),
                Position + new Vec2(Size.X * 1.5, 0) * Dir - new Vec2(Size.X * 3, Size.Y / 2), Color.Gray);
    }

    public void UpdateSword(double dt)
    {
        if (Controller.NeedSwing() && _EnableSword)
        {
            t = new Timer(Delay, DoSwing);
        }
    }
}