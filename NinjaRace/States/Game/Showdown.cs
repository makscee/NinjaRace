using VitPro;
using VitPro.Engine;
using UI = VitPro.Engine.UI;
using System;
using System.Collections.Generic;

class Showdown : UI.State
{
    protected World World;
    bool finished = false;
    public Showdown() { }
    public Showdown(string level, bool first)
    {
        World = new World(level);

        if (first)
            World.Player2.Lives = 2;
        else World.Player1.Lives = 2;
        World.EffectsScreen.Add(new Hearts(World.Player1));
        World.EffectsScreen.Add(new Hearts(World.Player2));

        World.Player1.Respawn = World.Player1.ChangeSpawn + World.Player1.Respawn;
        World.Player2.Respawn = World.Player2.ChangeSpawn + World.Player2.Respawn;

        Program.Manager.PushState(this);
        Program.Manager.PushState(new PreShowdown(World));
    }

    public override void Render()
    {
        Draw.Clear(Color.Black);
        RenderState.Push();
        World.RenderSingle();
        RenderState.Pop();
        base.Render();
    }
    double T = 0, FinishTimeout = 3;
    public override void Update(double dt)
    {
        if (finished)
        {
            T += dt;
            if (T > FinishTimeout)
            {
                Finish();
                return;
            }
            dt /= 7;
        }
        dt = Math.Min(dt, 1.0 / 60);
        base.Update(dt);
        World.Update(dt);
        if ((World.Player1.Lives < 1 || World.Player2.Lives < 1) && !finished)
        {
            finished = true;
            World.RenderScreenEffects = false;
        }
    }
    protected virtual void Finish()
    {
        Player p = (World.Player1.Lives < 1 ? World.Player2 : World.Player1);

        Texture tex = new Texture(RenderState.Width, RenderState.Height);
        RenderState.BeginTexture(tex);
        Render();
        RenderState.EndTexture();
        TimerContainer.Clear();
        Program.Manager.NextState = new GameOver(p);
    }
    public override void KeyDown(Key key)
    {
        World.KeyDown(key);
        if (key == Key.Escape)
        {
            TimerContainer.Clear();
            Close();
        }
    }

    public override void KeyUp(Key key)
    {
        World.KeyUp(key);
    }

}