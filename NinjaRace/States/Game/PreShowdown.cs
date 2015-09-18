using VitPro;
using VitPro.Engine;
using System;

class PreShowdown : VitPro.Engine.UI.State
{
    World World;
    Texture Showdown;
    double Time = 0;
    View cam = new View(240);
    public PreShowdown(World world)
    {
        World = world;
        Showdown = new Texture("./Data/img/showdown.png");
    }

    void Sign()
    {
        double T1, T2 = 1;
        if (Time < 3)
            return;
        else T1 = Time - 3;
        if (Time > 4)
            T2 = 5 - Time;
        RenderState.Push();
        RenderState.Color = new Color(1, 1, 1, T2);
        RenderState.Push();
        RenderState.BeginArea(new Vec2i(0, RenderState.Height / 2),
            new Vec2i(RenderState.Width, RenderState.Height / 2));
        RenderState.Translate(T1 < 1 ? 2 - T1 * 2 : 0, -1);
        Draw.Texture(Showdown, new Vec2(-1, -0.3), new Vec2(1, 0.3));
        RenderState.EndArea();
        RenderState.Pop();

        RenderState.Push();
        RenderState.BeginArea(new Vec2i(0, 0),
            new Vec2i(RenderState.Width, RenderState.Height / 2));
        RenderState.Translate(T1 < 1 ? -2 + T1 * 2 : 0, 1);
        Draw.Texture(Showdown, new Vec2(-1, -0.3), new Vec2(1, 0.3));
        RenderState.EndArea();
        RenderState.Pop();
        RenderState.Pop();
    }

    void Players()
    {
        Vec2 PlayersMed = World.player2.Position - World.player1.Position;
        if (Time < 1)
        {
            cam.Position = World.player1.Position + PlayersMed / 2;
            cam.FOV = 240 + 200 * Time;
        }
        else if (Time < 2)
        {
            cam.Position = World.player1.Position + PlayersMed * Math.Pow((1 - (Time - 1)), 2) / 2;
            if (Time < 1.8)
                cam.FOV = 440 - (Time - 1) * 400;
        }
        else if (Time < 4)
        {
            cam.Position = World.player2.Position - PlayersMed * Math.Pow((4 - Time) / 2, 2);
        }
        else
        {
            cam.Position = World.player1.Position + PlayersMed / 2 + PlayersMed * Math.Pow((1 - (Time - 4)), 2) / 2;
            cam.FOV = 120 + (Time - 4) * 400;
        }
        RenderState.Push();
        cam.Apply();
        World.level.Render();
        World.player1.Render();
        World.player2.Render();
        RenderState.Pop();
    }

    public override void Update(double dt)
    {
        base.Update(dt);
        Time += dt;
    }

    public override void Render()
    {
        Draw.Clear(Color.Black);
        base.Render();
        if (Time < 5)
        {
            Players();
            Sign();
        }
        else Close();
    }

    public override void KeyDown(Key key)
    {
        if (key == Key.Escape)
            Close();
    }
}