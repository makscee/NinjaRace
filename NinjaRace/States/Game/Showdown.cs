using VitPro;
using VitPro.Engine;
using UI = VitPro.Engine.UI;
using System;
using System.Collections.Generic;

class Showdown : UI.State
{
    World World;
    bool finished = false;
    public Showdown(string level, bool first)
    {
        World = new World(level);

        World.player2.Dir = -1;
        World.player1.Dir = 1;

        if (first)
            World.player2.Lives = 2;
        else World.player1.Lives = 2;
        World.EffectsScreen.Add(new Hearts(World.player1));
        World.EffectsScreen.Add(new Hearts(World.player2));

        World.player1.Respawn = World.player1.ChangeSpawn + World.player1.Respawn;
        World.player2.Respawn = World.player2.ChangeSpawn + World.player2.Respawn;

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
        if (finished)
        {
            dt /= 7;
        }
        base.Update(dt);

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