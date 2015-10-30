using VitPro;
using VitPro.Engine;
using System;
using System.Collections.Generic;

class JumpTileEffect : Effect
{
    List<PixelParticle> Particles = new List<PixelParticle>();
    double Life = 0.5, Time = 0;
    public JumpTileEffect(Vec2 pos)
        : base(pos)
    {
        double height = 200;
        int amount = 30;
        Color color = new Color(0.1, 0.7, 0.2, 0);
        for (int i = 0; i < amount; i++)
        {
            PixelParticle particle = new PixelParticle();
            particle.Position = new Vec2(Program.Random.NextDouble(pos.X - Tile.Size.X, pos.X + Tile.Size.X),
                Program.Random.NextDouble(pos.Y + Tile.Size.Y, pos.Y + Tile.Size.Y + height));
            particle.Color = color;
            particle.NeedVel = Vec2.OrtY;
            particle.Acc = 900;
            particle.Speed = 150;
            particle.Size = new Vec2(2, 2);
            Particles.Add(particle);
        }
        SetDuration(Life);
    }

    public override void Update(double dt)
    {
        Time += dt;
        Particles.Update(dt);
        Particles.ForEach((PixelParticle p) => 
        {
            double t = Time > Life / 2 ? (1 - Time / Life) * 2 : (Time / Life) * 2;
            p.Color = new Color(p.Color.R, p.Color.G, p.Color.B, t);
            p.Size = new Vec2(2 - 2 * Time / Life, 2 - 2 * Time / Life);
        });
    }

    public override void Render()
    {
        Particles.Render();
    }
}