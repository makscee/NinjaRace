using VitPro;
using VitPro.Engine;
using System;
using System.Collections.Generic;

class JumpTileEffect : Effect
{
    List<PixelParticle> Particles = new List<PixelParticle>();
    double Life = 0.5, Time = 0;
    Vec2 Pos1, Pos2;
    public JumpTileEffect(Vec2 pos)
        : base(pos)
    {
        double height = 400;
        int amount = 30;
        Pos1 = new Vec2(pos.X - Tile.Size.X, pos.Y + Tile.Size.Y);
        Pos2 = new Vec2(pos.X + Tile.Size.X, pos.Y + Tile.Size.Y + height);
        Color color = new Color(0.1, 0.7, 0.2, 0);
        for (int i = 0; i < amount; i++)
        {
            PixelParticle particle = new PixelParticle();
            particle.Position = new Vec2(Program.Random.NextDouble(Pos1.X, Pos2.X),
                Program.Random.NextDouble(Pos1.Y, Pos2.Y));
            particle.Color = color;
            particle.NeedVel = Vec2.OrtY;
            particle.Acc = 900;
            particle.Speed = 150;
            particle.Size = new Vec2(2, 2);
            Particles.Add(particle);
        }
        SetDuration(Life);
    }

    double T;
    public override void Update(double dt)
    {
        Time += dt;
        Particles.Update(dt);
        Particles.ForEach((PixelParticle p) => 
        {
            T = Time > Life / 2 ? (1 - Time / Life) * 2 : (Time / Life) * 2;
            p.Color = new Color(p.Color.R, p.Color.G, p.Color.B, T);
            p.Size = new Vec2(2 - 2 * Time / Life, 2 - 2 * Time / Life);
        });
    }

    public override void Render()
    {
        Draw.Rect(Pos1, Pos2, new Color(0, 0.5, 0.1, T / 8));
        Particles.Render();
    }
}