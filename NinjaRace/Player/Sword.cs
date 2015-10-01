using VitPro;
using VitPro.Engine;
using System.Collections.Generic;
using System;

partial class Player
{
    double Delay = 0.3;
    Timer t = new Timer(0, () => {});

    public Player GetOpponent()
    {
        return (Program.World.Player1 == this) ? Program.World.Player2 : Program.World.Player1;
    }

    public List<Player> GetAllOpponents()
    {
        List<Player> result = new List<Player>(Program.World.Copies[GetOpponent()]);
        result.Add(GetOpponent());
        return result;
    }

    void DoSwing()
    {
        if (!(States.IsDead))
            States.Set(new SwordHit(this));
    }

    public void UpdateSword(double dt)
    {
        if (Controller.NeedSwing() && t.IsDone)
        {
            t = new Timer(Delay, DoSwing);
        }
    }
}