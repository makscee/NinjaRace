using VitPro;
using VitPro.Engine;
using System;

class Walking : PlayerState
{
    AnimatedTexture idle, run;
    public Walking(Player player) : base(player) { }
    public override void Render()
    {
        Draw.Texture(GetTexture().GetCurrent(), player.Position - player.Size, player.Position + player.Size);
    }
    public override AnimatedTexture GetTexture()
    {
        if (player.Controller.NeedVel().X == 0)
        {
            if (idle == null)
            {
                idle = new AnimatedTexture();
                for (int i = 1; i < 20; i++)
                {
                    idle.Add(new Texture("./Data/img/player/idle/player" + i.ToString() + ".png"), 0.02);
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
                    run.Add(new Texture("./Data/img/player/run/player_run" + i.ToString() + ".png"), 0.02);
                }
            }
            return run;
        }
    }

}