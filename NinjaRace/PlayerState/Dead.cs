using VitPro;
using VitPro.Engine;
using System;

class Dead : PlayerState
{
    public Dead(Player player, Vec2 position) : base(player)
    {
        this.player = player;
        player.Velocity += (player.Position - position).Unit * player.JumpForce / 4;
    }

    private bool firstUp = false;
    public override void Update(double dt)
    {
        player.Velocity -= Vec2.Clamp(new Vec2(0, player.Velocity.Y + player.Gravity), player.GAcc * dt);
        if (!firstUp)
        {
            firstUp = true;
            return;
        }
        if (player.collisions[Side.Left].Count + player.collisions[Side.Down].Count + player.collisions[Side.Up].Count + player.collisions[Side.Right].Count != 0)
            player.Velocity = Vec2.Zero;
    }

    public override void Render()
    {
        Draw.Rect(player.Position + player.Size, player.Position - player.Size, Color.Gray);
    }

    public override void Jump()
    {
    }
}