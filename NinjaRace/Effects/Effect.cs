using System;
using VitPro;
using VitPro.Engine;

class Effect : IRenderable, IUpdateable, IDisposable
{
    public Vec2 Position;
    protected double Duration;

    public Effect(Vec2 pos, double duration)
    {
        Position = pos;
        Duration = duration;
    }

    public Effect(Vec2 pos) : this(pos, -1) { }
    public virtual void Render() { }
    public virtual void Update(double dt) 
    {
        if (Duration == -1)
            return;
        Duration -= dt;
        if (Duration < 0)
            Dispose();
    }

    public void Dispose()
    {
        IState current = ((MyManager)App.MainState).CurrentState;
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
            Program.World.EffectsBot.Refresh();
            Program.World.EffectsMid.Remove(this);
            Program.World.EffectsMid.Refresh();
            Program.World.EffectsTop.Remove(this);
            Program.World.EffectsTop.Refresh();
        }
    }
}