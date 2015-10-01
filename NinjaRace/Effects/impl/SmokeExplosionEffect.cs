using System;
using VitPro;
using VitPro.Engine;

class SmokeExplosionEffect : Effect
{
    ParticleExplosion<PixelParticle> Explosion;
    public SmokeExplosionEffect(Vec2 Position, double life = 0.6)
        : base(Position)
    {
        Explosion = (ParticleExplosion<PixelParticle>)new ParticleExplosion<PixelParticle>(25, life, Position)
            .AddParticleInitAction((PixelParticle p) =>
            {
                p.Color = Color.Gray;
                double t = Program.Random.NextDouble(1, 2);
                p.Acc = (t - 1) * 1200;
                p.Speed = 50 * t;
                p.Size = new Vec2(5 / t, 5 / t);
            })
        .AddParticleUpdateAction((PixelParticle p) =>
        {
            if (p.Time < life / 3)
                p.Color = new Color(p.Color.R, p.Color.G, p.Color.B, p.Time / (life / 3));
            else if (p.Time > life * 2 / 3)
                p.Color = new Color(p.Color.R, p.Color.G, p.Color.B, 1 - (p.Time - life * 2 / 3) / (life / 3));
        });
        SetDuration(life);
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