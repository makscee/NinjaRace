using VitPro;
using VitPro.Engine;
using System;

class ExpandEffect : Effect
{
    Group<PixelParticle> Particles;
    int Amount = 40;
    double Width = 70;
    public VitPro.Engine.UI.Element Element;
    public ExpandEffect(VitPro.Engine.UI.Element element)
        : base(element.Position)
    {
        Element = element;
        Particles = new Group<PixelParticle>();
    }

    public override void Update(double dt)
    {
        base.Update(dt);
        if (Alive)
            AddParticles();
        else if (Particles.Count == 0)
            Dispose();
        Particles.Refresh();
        foreach (var a in Particles)
        {
            double size = (Element.BottomLeft.X - a.Position.X) / Width, t = 4;
            a.Size = new Vec2(t, t) * (1 - size);
            a.Update(dt);
        }
        Particles.RemoveAll(Predicate);
        Particles.Refresh();
    }

    private bool Predicate(PixelParticle p)
    {
        return p.Position.X > Element.BottomLeft.X;
    }

    public override void Render()
    {
        RenderState.Push();
        foreach (var a in Particles)
        {
            Vec2 v = a.Position;
            a.Color = Element.BackgroundColor;
            a.Render();
            a.Position = new Vec2(a.Position.X + Element.Size.X + (Element.BottomLeft.X - a.Position.X) * 2, a.Position.Y);
            a.Render();
            a.Position = v;
        }
        RenderState.Pop();
    }

    void AddParticles()
    {
        for (int i = 0; i < Amount - Particles.Count; i++)
        {
            PixelParticle p = new PixelParticle();
            p.Position = Element.BottomLeft - new Vec2(Program.Random.NextDouble(0, Width),
                Program.Random.NextDouble(0, - Element.Size.Y));
            p.NeedVel = Vec2.OrtX;
            p.Speed = 350;
            p.Acc = 900;
            p.Size = new Vec2(2, 2);
            p.Color = Color.White;
            Particles.Add(p);
        }
    }

    public bool Alive = true;
}