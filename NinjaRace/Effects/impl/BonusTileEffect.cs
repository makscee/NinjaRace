using System;
using VitPro;
using VitPro.Engine;

class BonusTileEffect : Effect
{
    ParticleEngine<PixelParticle> Engine;
    public BonusTileEffect(BonusTile tile)
        : base(tile.Position)
    {
        double life = 0.3;
        Engine = new ParticleEngine<PixelParticle>(0.05, life, (ParticleEngine<PixelParticle> e) => { e.SetPosition(tile.Position); })
            .AddParticleInitAction((PixelParticle p) =>
            {
                p.Color = tile.Color;
                double t = Program.Random.NextDouble(1, 2);
                p.Acc = 1900;
                p.Speed = 100 * t;
                p.NeedVel = Vec2.OrtY;
            })
            .AddParticleUpdateAction((PixelParticle p) =>
            {
                p.Color = tile.Color;
                p.Size = new Vec2(life - p.Time, life - p.Time) * 7;
            })
            .SetSize(Tile.Size * 0.7)
            .SetProduceAmount(1);
    }

    public override void Update(double dt)
    {
        base.Update(dt);
        Engine.Update(dt);
    }

    public override void Render()
    {
        base.Render();
        Engine.Render();
    }
}