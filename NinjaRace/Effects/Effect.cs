using System;
using VitPro;
using VitPro.Engine;

class Effect : IRenderable, IUpdateable
{
    Vec2 Position;

    public Effect(Vec2 pos)
    {
        Position = pos;
    }

    public void Render() { }
    public void Update(double dt) { }
}