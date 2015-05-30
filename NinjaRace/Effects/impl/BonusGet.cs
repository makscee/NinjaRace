using System;
using VitPro;
using VitPro.Engine;
using System.Collections.Generic;

class BonusGet : Effect
{
    Group<GlowingParticle> particles = new Group<GlowingParticle>();
    Player player;

    public BonusGet(Vec2 pos, Player player)
        : base(pos)
    {
        this.player = player;
        for (int i = 0; i < 15; i++)
        {
            particles.Add(new GlowingParticle(pos, new Vec2(15, 15), Color.Green)
                .SetVelocity(Vec2.Rotate(Vec2.OrtX * 500 * Program.Random.NextDouble(1, 2), Math.PI / 15 * i))
                .SetSpeed(2000)
                .SetAcc(30));
        }
    }

    public override void Update(double dt)
    {
        foreach (var a in particles)
        {
            a.SetNeedVelocity(player.Position - a.Position);
            a.Update(dt);
            if ((a.Position - player.Position).Length < 20)
            {
                a.Dispose();
                particles.Remove(a);
            }
        }
        particles.Refresh();
        if (particles.Count == 0)
            Dispose();
    }

    public override void Render()
    {
        particles.Render();
    }
}