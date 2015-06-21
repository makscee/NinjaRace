using VitPro;
using VitPro.Engine;
using System;

class Showdown : State
{
    World World;

    public Showdown(string level, bool first)
    {
        World = new World(level);
        World.player1.EnableSword();
        World.player2.EnableSword();
        if (first)
            World.player2.lives = 2;
        else World.player1.lives = 2;
        World.EffectsScreen.Add(new Hearts(World.player1));
        World.EffectsScreen.Add(new Hearts(World.player2));
    }

    public override void Render()
    {
        Draw.Clear(Color.Black);
        World.RenderSingle();
    }
    public override void Update(double dt)
    {
        dt = Math.Min(dt, 1.0 / 60);
        World.Update(dt);
        TimerContainer.Update(dt);
        if (World.player1.lives < 1)
            Program.Manager.NextState = new GameOver("PLAYER1");
        if (World.player2.lives < 1)
            Program.Manager.NextState = new GameOver("PLAYER2");
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