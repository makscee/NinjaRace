using VitPro;
using VitPro.Engine;
using System;

class PlayerState : IRenderable, IUpdateable
{
    protected Player player;
    protected double time = 0;
    public bool Animated = false;
    public PlayerState(Player Player)
    {
        this.player = Player;
    }

    public virtual AnimatedTexture GetTexture()
    {
        return null;
    }

    public virtual void Render()
    {
        
    }

    public virtual void Update(double dt)
    {
        player.Velocity -= Vec2.Clamp(new Vec2(player.Velocity.X - player.Controller.NeedVel().X * player.Speed, 0), player.Acc * dt);
    }
    public virtual void Jump()
    {
        player.States.SetFlying();
        player.Velocity = new Vec2(player.Velocity.X, player.JumpForce);
    }
    public virtual void AbilityUse(Ability ability)
    {
        ability.Use();
    }

    public virtual void SpeedUp(Vec2 dir)
    {

    }

    public virtual void TileJump()
    {
        player.States.SetFlying();
        player.Velocity = new Vec2(0, player.JumpForce * 2);
    }

    public virtual void Die(Vec2 position)
    {
        player.States.SetDead();
        player.Velocity += (player.Position - position).Unit * player.JumpForce;
    }

    public void AddTime(double dt)
    {
        time += dt;
    }

    public double GetTime()
    {
        return time;
    }

    public void Reset()
    {
        time = 0;
    }
}