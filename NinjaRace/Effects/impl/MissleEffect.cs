using VitPro;
using VitPro.Engine;
using System;

class MissleEffect : Effect
{
    public Player Player;
    public PixelParticle MainParticle;
    ParticleEngine<PixelParticle> Engine;
    public MissleEffect(Player player)
        : base(player.Position)
    {
        Player = player.GetOpponent();
        MainParticle = new PixelParticle();
        MainParticle.Acc = 2400;
        MainParticle.Speed = 800;
        MainParticle.Size = new Vec2(2, 2);
        MainParticle.Position = player.Position;
        MainParticle.Color = Color.Red;
        Engine = new ParticleEngine<PixelParticle>(0.01, 0.3, (ParticleEngine<PixelParticle> e) => { e.SetPosition(MainParticle.Position); })
         .AddParticleInitAction((PixelParticle p) =>
         {
             p.NeedVel = Vec2.Rotate((Player.Position - MainParticle.Position).Unit
                 , Program.Random.NextDouble(-Math.PI / 3, Math.PI / 3));
             p.Color = Color.Red;
             p.Acc = 1200;
             p.Speed = 800;
         })
         .AddParticleUpdateAction((PixelParticle p) =>
         {
             p.Color = new Color(0.5 + (0.5 - p.Time * 1.6), p.Time * 2.5, p.Color.B, p.Color.A);
             if (p.Time > 0.25)
                 p.Color = new Color(p.Color.R, p.Color.G, p.Color.B, 1 - (p.Time - 0.25) * 20);
         })
         .SetProduceAmount(3);
    }

    public override void Update(double dt)
    {
        base.Update(dt);
        MainParticle.NeedVel = (Player.Position - MainParticle.Position).Unit;
        MainParticle.Update(dt);
        if ((MainParticle.Position - Player.Position).Length < 10)
        {
            this.Dispose();
            Player.States.current.Die(MainParticle.Position);
            Program.Statistics.Kills[Program.WhichPlayer(Player.GetOpponent())]++;
        }
        Engine.Update(dt);
    }

    public override void Render()
    {
        MainParticle.Render();
        Engine.Render();
    }
}