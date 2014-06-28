using System;
using VitPro.Engine;
using VitPro;

class WallGrab : PlayerState
{
    Side side;
    public WallGrab(Player player, Side side) : base(player) 
    {
        Player.Velocity = Vec2.Zero;
        this.side = side;
    }

    public override void Render()
    {
        if (side == Side.Left)
        {
            Draw.Rect(Player.Position + Player.Size, Player.Position - Player.Size, Color.Red);
            Draw.Rect(Player.Position - Player.Size, Player.Position - new Vec2(Player.Size.X / 2, -Player.Size.Y), Color.White);
        }
        else
        {
            Draw.Rect(Player.Position + Player.Size, Player.Position - Player.Size, Color.Red);
            Draw.Rect(Player.Position + new Vec2(Player.Size.X, -Player.Size.Y), Player.Position - new Vec2(-Player.Size.X / 2, -Player.Size.Y), Color.White);
        }
    }

    double DropSpeed = 50, DropAcc = 150;

    public override void Update(double dt)
    {
        Player.Velocity -= Vec2.Clamp(Player.Velocity - new Vec2(0, -1) * DropSpeed, DropAcc * dt);
        if ((Player.Controller.NeedVel().X == 1 && side == Side.Left) || (Player.Controller.NeedVel().X == -1 && side == Side.Right))
        {
            Player.Position += Player.Controller.NeedVel();
            Player.State = new Flying(Player);
        }

    }
    public override void Jump()
    {
        if (side == Side.Left)
            Player.Velocity = Vec2.Rotate(new Vec2(0, Player.JumpForce), -Math.PI / 4) * 1.5;
        else Player.Velocity = Vec2.Rotate(new Vec2(0, Player.JumpForce), Math.PI / 4) * 1.5;
        Player.State = new Flying(Player);
    }
}