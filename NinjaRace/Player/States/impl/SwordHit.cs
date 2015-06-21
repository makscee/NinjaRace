using VitPro;
using VitPro.Engine;
using System;

class SwordHit : PlayerState
{
    AnimatedTexture tex;
    public override AnimatedTexture GetTexture()
    {
        if (tex == null)
        {
            tex = new AnimatedTexture();
            tex.Add(new Texture("./Data/img/player/sword/sword1.png"), 0.04);
            tex.Add(new Texture("./Data/img/player/sword/sword2.png"), 0.04);
        }
        return tex;
    }

    public SwordHit(Player player) : base(player) { }

    public override void Render()
    {
        Vec2 pos1 = player.Dir == 1 ? player.Position - player.Size : player.Position - Vec2.CompMult(player.Size, new Vec2(3, 1));
        Vec2 pos2 = player.Dir == -1 ? player.Position + player.Size : player.Position + Vec2.CompMult(player.Size, new Vec2(3, 1));
        RenderState.Push();
        RenderState.Color = player.Color;
        RenderState.Translate(pos1);
        RenderState.Scale(pos2 - pos1);
        if (player.Dir == -1)
            RenderState.SetOrts(-Vec2.OrtX, Vec2.OrtY, new Vec2(1, 0));
        GetTexture().Render();
        RenderState.Pop();
    }

    public override void Update(double dt)
    {
        base.Update(dt);
        if (new CollisionBox(player.Position + new Vec2(player.Size.X * 1.5, 0) * player.Dir,
            new Vec2(player.Size.X * 3, player.Size.Y / 2))
            .Collide(player.GetOpponent().Box) != Side.None)
        {
            player.GetOpponent().States.current.Die(player.Position);
        }
        if (GetTime() > 0.08)
            player.States.Reset();
    }
}