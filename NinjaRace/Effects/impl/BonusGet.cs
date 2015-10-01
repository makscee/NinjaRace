using System;
using VitPro;
using VitPro.Engine;
using System.Collections.Generic;

class BonusGet : Effect
{
    Texture Tex;
    Timer Timer;
    public BonusGet(Vec2 pos, Bonus bonus)
        : base(pos)
    {
        Tex = bonus.GetTexture().Copy();
        Timer = new Timer(1, () => { this.Dispose(); Program.World.EffectsMid.Add(new BonusGetExplosionEffect(Position)); });
    }

    public override void Update(double dt)
    {
        Position = Position + new Vec2(0, 30 * dt);
    }

    public override void Render()
    {
        RenderState.Push();
        RenderState.Color = new Color(1, 1, 1, 1 - Timer.Elapsed * Timer.Elapsed);
        Draw.Texture(Tex, Position - Tile.Size, Position + Tile.Size);
        RenderState.Pop();
    }
}