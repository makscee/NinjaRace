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
    double d = 0;
    public override void Render()
    {
        Texture t = GetTexture().GetCurrent();
        Vec2 v = new Vec2(player.Controller.NeedVel().X != 0 ? Math.Round(d) * 2 : 0, 0);
        RenderState.Push();
        RenderState.Color = player.Color;
        RenderState.Translate(player.Position + v - player.Size);
        RenderState.Scale(player.Size * 2);
        if (player.Dir == -1)
            RenderState.SetOrts(-Vec2.OrtX, Vec2.OrtY, new Vec2(1, 0));
        t.Render();
        RenderState.Pop();
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
    public override void Update(double dt)
    {
        base.Update(dt);
        d  = d > 1 ? 0 : d + 7 * dt;
    }
}