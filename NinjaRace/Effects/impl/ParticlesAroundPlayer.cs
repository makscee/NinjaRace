using VitPro;
using VitPro.Engine;
using System;
using System.Collections.Generic;

class ParticlesAroundPlayer : Effect
{
    List<GlowingParticle> particles = new List<GlowingParticle>();
    List<double> radiuses = new List<double>();
    List<double> angles = new List<double>();

    Player player;

    public ParticlesAroundPlayer(double duration, Color color, Player player)
        : base(player.Position, duration)
    {
        this.player = player;
        for (int i = 0; i < 12; i++)
        {
            particles.Add(new GlowingParticle(player.Position, new Vec2(10, 10), duration, color));
            radiuses.Add(20 + 30 / 12 * i);
            angles.Add(Program.Random.NextDouble(0, Math.PI * 2));
        }
    }

    public override void Update(double dt)
    {
        for (int i = 0; i < particles.Count; i++)
        {
            angles[i] += dt * 3.5 * (radiuses[i]) / 50;
            Vec2 v = player.Position + Vec2.Rotate(new Vec2(1, 0), angles[i]) * radiuses[i] - particles[i].Position;
            particles[i].SetVelocity(player.Position + Vec2.Rotate(new Vec2(1, 0), angles[i]) * radiuses[i] - particles[i].Position);
            if (v.Length < particles[i].Speed * dt)
                particles[i].SetPosition(particles[i].Position + v);
            else particles[i].Update(dt);
        }
    }

    public override void Render()
    {
        particles.Render();
    }
}