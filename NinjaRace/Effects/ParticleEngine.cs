using VitPro;
using VitPro.Engine;
using System;

class ParticleEngine<T> : IUpdateable, IRenderable where T : Particle
{
    Vec2? Size;
    Vec2 Position;
    Group<T> Particles = new Group<T>();
    double Frequency, Life;
    double Time = 0;
    protected int ProduceAmount = 1;

    public ParticleEngine(double frequency, double life, Vec2 position)
    {
        Frequency = frequency;
        Life = life;
        Position = position;
    }
    public ParticleEngine(double frequency, double life, Action<ParticleEngine<T>> engineUpdate)
    {
        Frequency = frequency;
        Life = life;
        EngineUpdateAction = engineUpdate;
    }
    public virtual void Update(double dt)
    {
        Time += dt;
        if (ParticleUpdateAction != null)
            foreach(var p in Particles)
                ParticleUpdateAction.Apply(p);
        if (EngineUpdateAction != null)
            EngineUpdateAction.Apply(this);
        if (Time > Frequency)
        {
            Time = 0;
            for (int i = 0; i < ProduceAmount; i++)
                AddParticle((T)Activator.CreateInstance(typeof(T), new object[] { }));
        }
        Particles.Update(dt);
        Particles.Refresh();
    }
    public void Render()
    {
        Particles.Render();
    }
    public ParticleEngine<T> SetSize(Vec2 size)
    {
        Size = size;
        return this;
    }
    public ParticleEngine<T> SetPosition(Vec2 position)
    {
        Position = position;
        return this;
    }
    public ParticleEngine<T> SetProduceAmount(int amount)
    {
        ProduceAmount = amount;
        return this;
    }
    Action<T> ParticleInitAction = (T p) => { };
    Action<T> ParticleUpdateAction = (T p) => { };
    Action<ParticleEngine<T>> EngineUpdateAction = (ParticleEngine<T> e) => { };
    public ParticleEngine<T> AddParticleInitAction(Action<T> a)
    {
        ParticleInitAction += a;
        return this;
    }
    public ParticleEngine<T> AddParticleUpdateAction(Action<T> a)
    {
        ParticleUpdateAction += a;
        return this;
    }
    protected void AddParticle(T p)
    {
        Particles.Add(p);
        if (ParticleInitAction != null)
            ParticleInitAction.Apply(p);
        p.Position = Size == null ? Position :
            Position + new Vec2(Size.Value.X * Program.Random.NextDouble(-1, 1), Size.Value.Y * Program.Random.NextDouble(-1, 1));
        new Timer(Life, () => { Particles.Remove(p); });
    }
}