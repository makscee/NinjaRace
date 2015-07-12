using VitPro;
using VitPro.Engine;
using System;

class Jump : Falling
{
    public Jump(Player player)
        : base(player)
    {
        CanJump = true;
        tex = new AnimatedTexture();
        tex.Loopable = false;
        tex.Add(new Texture("./Data/img/player/jump/jump1.png"), 0.5);
        tex.Add(new Texture("./Data/img/player/jump/jump2.png"), 0.05);
    }

    AnimatedTexture tex;
    public override AnimatedTexture GetTexture()
    {
        return tex;
    }

    public override void Update(double dt)
    {
        base.Update(dt);
        if (player.Velocity.Y < 0)
            player.States.SetFalling();
    }

    public override void Reset()
    {
        base.Reset();
        player.Velocity = new Vec2(player.Velocity.X, player.JumpForce);
    }
}