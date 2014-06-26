using VitPro;
using VitPro.Engine;
using System;

class Flying : PlayerState
{
    double Gravity = 6000, Acc = 1200;
    public Flying(Player player) : base(player) { }
    public override void Update(double dt)
    {
        Player.Velocity -= Vec2.Clamp(new Vec2(Player.Velocity.X - Player.Controller.NeedVel().X * Speed, 0), Acc * dt);
        Player.Velocity -= Vec2.Clamp(new Vec2(0, Player.Velocity.Y + Gravity), Acc * dt);
    }
    public override void Render()
    {
        Draw.Rect(Player.Position + Player.Size, Player.Position - Player.Size, Color.Red);
    }
}