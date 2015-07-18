using VitPro;
using VitPro.Engine;
using System;

class Walking : PlayerState
{
    AnimatedTexture idle, run;
    public Walking(Player player) : base(player) 
    {
        idle = new AnimatedTexture();
        for (int i = 1; i < 20; i++)
        {
            idle.Add(new Texture("./Data/img/player/idle/player" + i.ToString() + ".png"), 0.03);
        }
        run = new AnimatedTexture();
        for (int i = 1; i < 7; i++)
        {
            run.Add(new Texture("./Data/img/player/run/player_run" + i.ToString() + ".png"), 0.03);
        }
    }
    public override AnimatedTexture GetTexture()
    {
        if (player.Controller == null || player.Controller.NeedVel().X == 0)
        {
            return idle;
        }
        else
        {
            return run;
        }
    }

}