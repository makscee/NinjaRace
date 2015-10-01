using VitPro;
using VitPro.Engine;
using System;

class SpeedUpEffect : Effect
{
    Player Player;
    ParticleEngine<PixelParticle> Engine;
    public SpeedUpEffect(Player player)
        : base(player.Position)
    {
        Player = player;
        Engine = new ParticleEngine<PixelParticle>(0.05, 0.3, (ParticleEngine<PixelParticle> e) => { e.SetPosition(player.Position); })
        .AddParticleInitAction((PixelParticle p) => 
            {
                p.NeedVel = Vec2.Rotate(new Vec2(-player.Dir, 0), Program.Random.NextDouble(-Math.PI / 6, Math.PI / 6));
                p.Color = Color.Green;
                p.Acc = 1200;
                p.Speed = 300;
            })
        .AddParticleUpdateAction((PixelParticle p) =>
            {
                p.NeedVel = player.Dir * p.NeedVel.X < 0 ? p.NeedVel : -p.NeedVel;
                if (p.Time < 0.05)
                    p.Color = new Color(p.Color.R, p.Color.G, p.Color.B, p.Time * 20);
                else if (p.Time > 0.25)
                    p.Color = new Color(p.Color.R, p.Color.G, p.Color.B, 1 - (p.Time - 0.25) * 20);
            })
        .SetSize(player.Size)
        .SetProduceAmount(2);
    }

    public override void Render()
    {
        base.Render();
        Engine.Render();
    }
    public override void Update(double dt)
    {
        base.Update(dt);
        Engine.Update(dt);
    }
}