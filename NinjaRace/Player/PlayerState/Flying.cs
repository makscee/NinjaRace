using VitPro;
using VitPro.Engine;
using System;

class Flying : PlayerState
{
    public Flying(Player player) : base(player) { }
    public override void Update(double dt)
    {
        player.Velocity -= Vec2.Clamp(new Vec2(player.Velocity.X - player.Controller.NeedVel().X * player.Speed, 0), player.Acc * dt);
        player.Velocity -= Vec2.Clamp(new Vec2(0, player.Velocity.Y + player.Gravity), player.GAcc * dt);
    }
    public override void Render()
    {
        Draw.Rect(player.Position + player.Size, player.Position - player.Size, Color.Red);
    }
    public override void Jump()
    {
    }
}