using VitPro;
using VitPro.Engine;
using System;

partial class Player
{
    double Delay = 0.3;
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
        if (!(States.current is Dead))
            States.Set(new SwordHit(this));
    }

    public void UpdateSword(double dt)
    {
        if (Controller.NeedSwing() && _EnableSword)
        {
            t = new Timer(Delay, DoSwing);
        }
    }
}