using VitPro;
using VitPro.Engine;
using System;

class Win : PlayerState
{
    public Win(Player player) : base(player)
    {
        this.player = player;
    }

    double t = 0;
    Color color = Color.Red;
    public override void Update(double dt)
    {
        t += dt;
        player.Velocity -= Vec2.Clamp(new Vec2(0, player.Velocity.Y + player.Gravity), player.GAcc * dt);
        if (t > 0.3)
        {
            t = 0;
            color = new Color(Program.Random.NextDouble(), Program.Random.NextDouble(), Program.Random.NextDouble());
        }
    }
    public override void Render()
    {
        Draw.Rect(player.Position + player.Size, player.Position - player.Size, color);
    }

    public override void Jump()
    {
    }

    public override void AbilityUse(Ability ability)
    {
    }

    public override void TileJump() { }

    public override void Die(Vec2 position) { }
}