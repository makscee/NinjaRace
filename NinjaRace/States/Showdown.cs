using VitPro;
using VitPro.Engine;
using UI = VitPro.Engine.UI;
using System;

class Showdown : UI.State
{
    World World;
    bool started = false;
    Timer t;
    Label time = new Label("3", 70);
    public Showdown(string level, bool first)
    {
        World = new World(level);
        World.player1.EnableSword();
        World.player2.EnableSword();
        World.player1.States.SetWalking();
        World.player2.States.SetWalking();

        World.player2.Dir = -1;
        World.player1.Dir = 1;

        if (first)
            World.player2.Lives = 2;
        else World.player1.Lives = 2;
        World.EffectsScreen.Add(new Hearts(World.player1));
        World.EffectsScreen.Add(new Hearts(World.player2));
        t = new Timer(0, () => { started = true; Frame.Remove(time); });
        time.Anchor = new Vec2(0.5, 0.5);
        Frame.Add(time);

        World.player1.Update(0);
        World.player2.Update(0);
    }

    public override void Render()
    {
        Draw.Clear(Color.Black);
        World.RenderSingle();
        base.Render();
    }
    public override void Update(double dt)
    {
        base.Update(dt);
        TimerContainer.Update(dt);

        if (!started)
        {
            time.Text = Math.Round(3 - t.Elapsed).ToString();
            return;
        }
        dt = Math.Min(dt, 1.0 / 60);
        World.Update(dt);
        if (World.player1.Lives < 1 || World.player2.Lives < 1)
        {
            string s = "PLAYER" + (World.player1.Lives < 1 ? "2" : "1");

            Texture tex = new Texture(RenderState.Width, RenderState.Height);
            RenderState.BeginTexture(tex);
            Render();
            RenderState.EndTexture();
            Program.Manager.NextState = new GameOver(s, tex);
        }
    }
    public override void KeyDown(Key key)
    {
        World.KeyDown(key);
        if (key == Key.Escape)
            Close();
    }

    public override void KeyUp(Key key)
    {
        World.KeyUp(key);
    }

}