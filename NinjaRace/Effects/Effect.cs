using System;
using System.Diagnostics;
using VitPro;
using VitPro.Engine;

class Effect : IRenderable, IUpdateable, IDisposable
{
    public Vec2 Position;
    protected double Duration;
    public Stopwatch sw = new Stopwatch();

    public Effect(Vec2 pos)
    {
        Position = pos;
    }

    public Effect SetDuration(double d)
    {
        sw.Start();
        Duration = d;
        return this;
    }

    public virtual void Render() { }
    public virtual void Update(double dt) 
    {
        if (!sw.IsRunning)
            return;
        if (sw.ElapsedMilliseconds / 1000 > Duration)
            Dispose();
    }

    public virtual void Dispose()
    {
        State current = ((MyManager)((State.Manager)App.State).CurrentState).CurrentState;
        if (current is Menu)
        {
            Menu menu = (Menu)current;
            menu.EffectsTop.Remove(this);
            menu.EffectsBot.Remove(this);
            menu.EffectsBot.Refresh();
            menu.EffectsTop.Refresh();
        }
        if (current is Game || current is Showdown)
        {
            Program.World.EffectsBot.Remove(this);
            Program.World.EffectsMid.Remove(this);
            Program.World.EffectsTop.Remove(this);
            Program.World.EffectsScreen.Remove(this);
        }
    }
}