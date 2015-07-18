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
            Close();
        else
        {
            T -= dt;
            Time.Text = Math.Ceiling(T).ToString();
        }
    }
    Camera cam = new Camera(180);
    void Player1()
    {
        double a = T - 2;
        cam.Position = World.player1.Position + new Vec2(40, 10 - a * 20);
        cam.Apply();
        Time.Anchor = new Vec2(0.7, 0.5);
        //RenderState.Color = new Color(1, 1, 1, a);
        World.Render(World.player1);
    }
    void Player2()
    {
        double a = T - 1;
        cam.Position = World.player2.Position - new Vec2(40, 10 - a * 20);
        cam.Apply();
        Time.Anchor = new Vec2(0.3, 0.5);
        //RenderState.Color = new Color(1, 1, 1, a);
        World.Render(World.player2);
    }
    void All()
    {
        Time.Anchor = new Vec2(0.5, 0.5);
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
}