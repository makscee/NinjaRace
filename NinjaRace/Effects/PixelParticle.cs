using VitPro;
using VitPro.Engine;
using System;

class PixelParticle : Particle
{
    public PixelParticle()
    {
        Speed = 100;
        Acc = 300;
        Size = new Vec2(1, 1);
    }
    public override void Render()
    {
        Draw.Rect(Position - Size, Position + Size, Color);
    }
}