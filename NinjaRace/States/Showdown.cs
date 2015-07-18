using VitPro;
using VitPro.Engine;
using UI = VitPro.Engine.UI;
using System;

class Showdown : UI.State
{
    World World;
    bool started = false, finished = false;
    Timer t;
    Label Time = new Label("3", 70);
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
        World.RenderScreenEffects = false;
        t = new Timer(2, () => { started = true; Frame.Remove(Time); World.RenderScreenEffects = true; });
        Time.Anchor = new Vec2(0.5, 0.5);
        Frame.Add(Time);

        World.player1.Update(0);
        World.player2.Update(0);

        Program.Manager.PushState(this);
        Program.Manager.PushState(new PreGame(World));
    }

    public override void Render()
    {
        Draw.Clear(Color.Black);
        RenderState.Push();
        World.RenderSingle();
        RenderState.Pop();
        base.Render();
    }
    public override void Update(double dt)
    {
        TimerContainer.Update(dt);
        if (finished)
        {
            dt /= 7;
        }
        base.Update(dt);

        if (!started)
        {
            Time.Text = Math.Round(3 - t.Elapsed).ToString();
            return;
        }
        dt = Math.Min(dt, 1.0 / 60);
        World.Update(dt);
        if ((World.player1.Lives < 1 || World.player2.Lives < 1) && !finished)
        {
            finished = true;
            World.RenderScreenEffects = false;
            new Timer(3, () =>
            {
                string s = "PLAYER" + (World.player1.Lives < 1 ? "2" : "1");

                Texture tex = new Texture(RenderState.Width, RenderState.Height);
                RenderState.BeginTexture(tex);
                Render();
                RenderState.EndTexture();
                Program.Manager.NextState = new GameOver(s, tex);
            });
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