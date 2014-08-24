using System;
using VitPro.Engine;
using VitPro;

class WallGrab : PlayerState
{
    Side side;
    public WallGrab(Player player, Side side) : base(player) 
    {
        player.Velocity = new Vec2(0, player.Velocity.Y);
        this.side = side;
    }

    public override void Render()
    {
        if (side == Side.Left)
        {
            Draw.Rect(player.Position + player.Size, player.Position - player.Size, Color.Red);
            Draw.Rect(player.Position - player.Size, player.Position - new Vec2(player.Size.X / 2, -player.Size.Y), Color.White);
        }
        else
        {
            Draw.Rect(player.Position + player.Size, player.Position - player.Size, Color.Red);
            Draw.Rect(player.Position + new Vec2(player.Size.X, -player.Size.Y), player.Position - new Vec2(-player.Size.X / 2, -player.Size.Y), Color.White);
        }
    }

    public override void Update(double dt)
    {
        player.Velocity -= Vec2.Clamp(player.Velocity - new Vec2(0, -1) * player.DropSpeed, player.DropAcc * dt);
        base.Update(dt);
    }
    public override void Jump()
    {
        if (side == Side.Left)
            player.Velocity = Vec2.Rotate(new Vec2(0, player.JumpForce), -Math.PI / 4) * 1.5;
        else player.Velocity = Vec2.Rotate(new Vec2(0, player.JumpForce), Math.PI / 4) * 1.5;
        player.State = new Flying(player);
    }
}