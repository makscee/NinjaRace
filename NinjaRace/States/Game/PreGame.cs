using VitPro;
using VitPro.Engine;
using System;

class PreGame : VitPro.Engine.UI.State
{
    World World;
    Label Time;
    public PreGame(World world)
    {
        World = world;
        Time = new Label("3", 240);
        Time.BackgroundColor = Color.TransparentBlack;
        Time.TextColor = Color.Yellow;
        Time.Anchor = new Vec2(0.5, 0.5);
        Frame.Add(Time);
    }

    double T = 3;

    public override void Update(double dt)
    {
        base.Update(dt);
        if (T - dt < 0)
        {
            TexP1.Dispose();
            TexP2.Dispose();
            Close();
        }
        else
        {
            T -= dt;
            Time.Text = Math.Ceiling(T).ToString();
        }
    }
    View cam = new View(220);
    Texture TexP1, TexP2;
    void Player1()
    {
        if (TexP1 == null)
        {
            TexP1 = new Texture(App.Width, App.Height);
            RenderState.BeginTexture(TexP1);
            cam.Position = World.Player1.Position;
            cam.Apply();
            World.Render(World.Player1);
            RenderState.EndTexture();
        }
        double a = T - 2;
        Time.Anchor = new Vec2(0.7, 0.5);
        RenderState.Translate(new Vec2(-0.3, 1.0 / 8 - a / 4));
        RenderState.Color = new Color(1, 1, 1, Math.Min(1, (0.5 - Math.Abs(a - 0.5)) * 3));
        RenderState.Scale(1.5);
        Draw.Texture(TexP1, new Vec2(-1, -1), new Vec2(1, 1));
    }
    void Player2()
    {
        if (TexP2 == null)
        {
            TexP2 = new Texture(App.Width, App.Height);
            RenderState.BeginTexture(TexP2);
            cam.Position = World.Player2.Position;
            cam.Apply();
            World.Render(World.Player2);
            RenderState.EndTexture();
        }
        double a = T - 1;
        Time.Anchor = new Vec2(0.3, 0.5);
        RenderState.Translate(new Vec2(0.3, - 1.0 / 8 + a / 4));
        RenderState.Color = new Color(1, 1, 1, Math.Min(1, (0.5 - Math.Abs(a - 0.5)) * 3));
        RenderState.Scale(1.5);
        Draw.Texture(TexP2, new Vec2(-1, -1), new Vec2(1, 1));
    }
    void All()
    {
        RenderState.Color = new Color(1, 1, 1, Math.Min(1, (0.5 - Math.Abs(T - 0.5)) * 3));
        Time.Anchor = new Vec2(0.5, 0.5);
        RenderState.Push();
        RenderState.BeginArea(new Vec2i(0, RenderState.Height / 2),
            new Vec2i(RenderState.Width, RenderState.Height / 2));
        RenderState.Translate(-0.5, 0);
        RenderState.Scale(2.5, 5);
        Draw.Texture(TexP1, new Vec2(-1, -1), new Vec2(1, 1));
        RenderState.EndArea();
        RenderState.Pop();

        RenderState.Push();
        RenderState.BeginArea(new Vec2i(0, 0),
            new Vec2i(RenderState.Width, RenderState.Height / 2));
        RenderState.Translate(0.5, 0);
        RenderState.Scale(2.5, 5);
        Draw.Texture(TexP2, new Vec2(-1, -1), new Vec2(1, 1));
        RenderState.EndArea();
        RenderState.Pop();
    }

    public override void Render()
    {
        RenderState.Push();
        Draw.Clear(Color.Black);
        if (T > 2)
            Player1();
        else if (T > 1)
            Player2();
        else All();
        RenderState.Pop();
        base.Render();
    }
    public override void KeyDown(Key key)
    {
        if (key == Key.Escape)
            Close();
    }
}