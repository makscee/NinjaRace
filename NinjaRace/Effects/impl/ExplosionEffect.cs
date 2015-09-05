using System;
using VitPro;
using VitPro.Engine;

class ExplosionEffect : Effect
{
    ParticleExplosion<PixelParticle> Explosion;
    public ExplosionEffect(Vec2 Position)
        : base(Position)
    {
        Explosion = (ParticleExplosion<PixelParticle>)new ParticleExplosion<PixelParticle>(15, 0.3, Position)
            .SetParticleInitAction((PixelParticle p) =>
        {
            p.Color = Color.Red;
            p.Acc = 2200;
            p.Speed = 1500;
        })
        .SetParticleUpdateAction((PixelParticle p) =>
        {
            if (p.Time < 0.1)
                p.Color = new Color(p.Color.R, p.Color.G, p.Color.B, p.Time * 10);
            else if (p.Time > 0.20)
                p.Color = new Color(p.Color.R, p.Color.G, p.Color.B, 1 - (p.Time - 0.20) * 10);
        });
    }

    public override void Update(double dt)
    {
        base.Update(dt);
        Explosion.Update(dt);
    }

    public override void Render()
    {
        base.Render();
        Explosion.Render();
    }
}
