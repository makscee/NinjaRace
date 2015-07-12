using VitPro;
using VitPro.Engine;
using System;

class DoubleJump : Falling
{
    public DoubleJump(Player player)
        : base(player)
    {
        CanJump = false;
        tex = new AnimatedTexture();
        tex.Loopable = false;
        for (int i = 1; i <= 8; i++)
        {
            tex.Add(new Texture("./Data/img/player/doublejump/doublejump" + i.ToString() + ".png"), 0.05);
        }
    }

    AnimatedTexture tex;
    public override AnimatedTexture GetTexture()
    {
        return tex;
    }

    public override void Render()
    {
        RenderState.Push();
        RenderState.Color = player.Color;
        RenderState.Translate(player.Position - player.Size);
        RenderState.Scale(Vec2.CompMult(player.Size, new Vec2(1.5, 1)) * 2);
        if (player.Dir == -1)
            RenderState.SetOrts(-Vec2.OrtX, Vec2.OrtY, new Vec2(1, 0));
        tex.Render();
        RenderState.Pop();
    }

    public override void Update(double dt)
    {
        base.Update(dt);
        if (tex.HasLooped)
            player.States.SetFalling(false);
    }

    public override void Jump()
    {
    }

    public override void Reset()
    {
        base.Reset();
        player.Velocity = new Vec2(player.Velocity.X, player.JumpForce);
        tex.Reset();
    }
}