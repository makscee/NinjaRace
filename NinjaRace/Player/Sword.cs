using VitPro;
using VitPro.Engine;
using System;
using System.Threading;

partial class Player
{
    long Delay = 500;
    Timer t;

    public Player GetOpponent()
    {
        return (Program.World.player1 == this) ? Program.World.player2 : Program.World.player1;
    }

    void DoSwing(Object state)
    {
        if (new CollisionBox(Position + new Vec2(Size.X * 1.5, 0) * Dir, new Vec2(Size.X * 3, Size.Y / 2))
            .Collide(GetOpponent().Box) != Side.None)
            GetOpponent().States.SetDead();
    }

    public void UpdateSword(double dt)
    {
        if (Controller.NeedSwing())
            t = new Timer(DoSwing, null, Delay, Timeout.Infinite);
    }
}