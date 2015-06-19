using VitPro;
using VitPro.Engine;
using System;

class Walking : PlayerState
{
    AnimatedTexture idle, run;
    public Walking(Player player) : base(player) { }
    public override AnimatedTexture GetTexture()
    {
        if (player.Controller.NeedVel().X == 0)
        {
            if (idle == null)
            {
                idle = new AnimatedTexture();
                for (int i = 1; i < 20; i++)
                {
                    idle.Add(new Texture("./Data/img/player/idle/player" + i.ToString() + ".png"), 0.03);
                }
            }
            return idle;
        }
        else
        {
            if (run == null)
            {
                run = new AnimatedTexture();
                for (int i = 1; i < 7; i++)
                {
                    run.Add(new Texture("./Data/img/player/run/player_run" + i.ToString() + ".png"), 0.03);
                }
            }
            return run;
        }
    }

}