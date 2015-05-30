using System;
using VitPro;
using VitPro.Engine;

class Effect : IRenderable, IUpdateable, IDisposable
{
    public Vec2 Position;
    protected double Duration = -1;

    public Effect(Vec2 pos)
    {
        Position = pos;
    }

    public virtual void Render() { }
    public virtual void Update(double dt) 
    {
        if (Duration == -1)
            return;
        Duration -= dt;
        if (Duration < 0)
            Dispose();
    }

    public virtual void Dispose()
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
            Program.World.EffectsMid.Remove(this);
            Program.World.EffectsTop.Remove(this);
            Program.World.EffctsScreen.Remove(this);
        }
    }
}