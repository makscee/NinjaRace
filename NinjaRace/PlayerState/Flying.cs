using VitPro;
using VitPro.Engine;
using System;

class Flying : PlayerState
{
    public Flying(Player player) : base(player) { }
    public override void Update(double dt)
    {
        Player.Velocity -= Vec2.Clamp(new Vec2(Player.Velocity.X - Player.Controller.NeedVel().X * Player.Speed, 0), Player.Acc * dt);
        Player.Velocity -= Vec2.Clamp(new Vec2(0, Player.Velocity.Y + Player.Gravity), Player.GAcc * dt);
    }
    public override void Render()
    {
        Draw.Rect(Player.Position + Player.Size, Player.Position - Player.Size, Color.Red);
    }
    public override void CollideWith(Tile t)
    {
        Side s = Player.Box.Collide(t.Box);
        if (s == Side.Right || s == Side.Left)
            Player.State = new WallGrab(Player, s);
        base.CollideWith(t);
    }
    public override void Jump()
    {
    }
}