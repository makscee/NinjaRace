using VitPro;
using VitPro.Engine;
using System;

abstract class Particle : IUpdateable, IRenderable
{
    public Vec2 Position, Vel, NeedVel, Size;
    public double Speed, Acc, Time = 0;
    public Color Color;

    public void Update(double dt)
    {
        Time += dt;
        Vel -= Vec2.Clamp(Vel - NeedVel * Speed, Acc * dt);
        Position += Vel * dt;
    }

    public abstract void Render();
}