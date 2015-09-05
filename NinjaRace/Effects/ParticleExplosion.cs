using System;
using VitPro;
using VitPro.Engine;

class ParticleExplosion<T> : ParticleEngine<T> where T : Particle
{
    bool ParticlesCreated = false;
    double BeginAngle, EndAngle;
    public ParticleExplosion(int amount, double life, Vec2 position, double beginAngle = 0, double endAngle = Math.PI * 2)
        : base(double.MaxValue, life, position)
    {
        BeginAngle = beginAngle;
        EndAngle = endAngle;
        ProduceAmount = amount;
    }

    public override void Update(double dt)
    {
        if (!ParticlesCreated)
        {
            for (int i = 0; i < ProduceAmount; i++)
            {
                T p = (T)Activator.CreateInstance(typeof(T), new object[] { });
                AddParticle(p);
                p.NeedVel = Vec2.Rotate(Vec2.OrtX, Program.Random.NextDouble(BeginAngle, EndAngle));
            }
            ParticlesCreated = true;
        }
        base.Update(dt);
    }
}
