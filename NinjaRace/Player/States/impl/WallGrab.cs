using System;
using VitPro.Engine;
using VitPro;

class WallGrab : PlayerState
{
    Side side;
    AnimatedTexture tex;
    public WallGrab(Player player) : base(player)
    {
        player.Velocity = new Vec2(0, player.Velocity.Y);
    }

    public override AnimatedTexture GetTexture()
    {
        if (tex == null)
            tex = new AnimatedTexture(new Texture("./Data/img/player/wallgrab/wallgrab.png"));
        return tex;
    }

    public override void Update(double dt)
    {
        if (player.collisions[Side.Left].Count != 0)
            side = Side.Left;
        else side = Side.Right;
        player.Dir = side == Side.Left ? -1 : 1;
        player.Velocity -= Vec2.Clamp(player.Velocity - new Vec2(0, -1) * player.SlideSpeed, player.SlideAcc * dt);
        base.Update(dt);
    }
    public override void Jump()
    {
        player.Dir = side == Side.Left ? 1 : -1;
        if (side == Side.Left)
            player.Velocity = Vec2.Rotate(new Vec2(0, player.JumpForce), -Math.PI / 4) * 1.5;
        else player.Velocity = Vec2.Rotate(new Vec2(0, player.JumpForce), Math.PI / 4) * 1.5;
        player.States.SetFlying();
    }
}