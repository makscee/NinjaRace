using VitPro;
using VitPro.Engine;
using System;

class PlayerState : IRenderable, IUpdateable
{
    protected Player Player;
    public PlayerState(Player Player)
    {
        this.Player = Player;
    }
    public virtual void Render()
    {
        Draw.Rect(Player.Position + Player.Size, Player.Position - Player.Size, Color.White);
    }

    public virtual void Update(double dt)
    {
        Player.Velocity -= Vec2.Clamp(new Vec2(Player.Velocity.X - Player.Controller.NeedVel().X * Player.Speed, 0), Player.Acc * dt);
    }
    public virtual void Jump()
    {
        Player.State = new Flying(Player);
        Player.Velocity = new Vec2(Player.Velocity.X, Player.JumpForce);
    }
}