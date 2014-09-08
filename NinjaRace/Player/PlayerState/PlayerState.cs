using VitPro;
using VitPro.Engine;
using System;

class PlayerState : IRenderable, IUpdateable
{
    protected Player player;
    public PlayerState(Player Player)
    {
        this.player = Player;
    }
    public virtual void Render()
    {
        Draw.Rect(player.Position + player.Size, player.Position - player.Size, Color.White);
    }

    public virtual void Update(double dt)
    {
        player.Velocity -= Vec2.Clamp(new Vec2(player.Velocity.X - player.Controller.NeedVel().X * player.Speed, 0), player.Acc * dt);
    }
    public virtual void Jump()
    {
        player.State = new Flying(player);
        player.Velocity = new Vec2(player.Velocity.X, player.JumpForce);
    }
    public virtual void AbilityUse()
    {
        player.Ability.Use();
    }

    public virtual void SpeedUp(Vec2 dir)
    {

    }

    public virtual void TileJump()
    {
        player.State = new Flying(player);
        player.Velocity = new Vec2(0, player.JumpForce * 2);
    }

    public virtual void Die(Vec2 position)
    {
        player.State = new Dead(player, position);
    }
}