using VitPro;
using VitPro.Engine;
using System;

class PlayerState : IRenderable, IUpdateable
{
    protected Player player;
    protected double time = 0;
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
        if(GetTexture() != null)
            player.RenderTex(GetTexture().GetCurrent());
    }

    public virtual void Update(double dt)
    {
        player.Velocity -= Vec2.Clamp(new Vec2(player.Velocity.X - player.Controller.NeedVel().X * player.Speed * player.SpeedUp, 0), player.Acc * player.SpeedUp * dt);
        AnimatedTexture tex = GetTexture();
        if (tex != null)
            tex.Update(dt);
    }
    public virtual void Jump()
    {
        player.States.Jump();
    }
    public virtual void Die(Vec2 position)
    {
        player.States.SetDead();
        if (Program.IsCopy(player))
        {
            Program.World.Copies[Program.World.Player1].Remove(player);
            Program.World.Copies[Program.World.Player2].Remove(player);
            Program.World.EffectsTop.Add(new SmokeExplosionEffect(player.Position));
            return;
        }
        else
        {
            foreach (var a in Program.World.Copies[player])
                a.States.current.Die(a.Position);
        }
        Program.Statistics.Deaths[Program.WhichPlayer(player)]++;
        player.Velocity += (player.Position - position).Unit * player.JumpForce;
        player.Lives--;
        Program.World.SlowTime = 0;
    }

    public void AddTime(double dt)
    {
        time += dt;
    }

    public double GetTime()
    {
        return time;
    }

    public virtual void Reset()
    {
        time = 0;
        if (GetTexture() != null)
            GetTexture().Reset();
    }
}