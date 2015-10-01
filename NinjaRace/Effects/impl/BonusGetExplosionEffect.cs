using System;
using VitPro;
using VitPro.Engine;

class BonusGetExplosionEffect : Effect
{
    ParticleExplosion<PixelParticle> Explosion;
    public BonusGetExplosionEffect(Vec2 Position, double life = 0.3)
        : base(Position)
    {
        Explosion = (ParticleExplosion<PixelParticle>)new ParticleExplosion<PixelParticle>(15, life, Position)
            .AddParticleInitAction((PixelParticle p) =>
        {
            p.Color = Color.Red;
            double t = Program.Random.NextDouble(1, 2);
            p.Acc = (t - 1) * 1900;
            p.Speed = 100 * t;
            p.Size = new Vec2(3 / t, 3 / t);
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
