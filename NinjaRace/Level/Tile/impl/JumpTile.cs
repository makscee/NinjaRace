using VitPro;
using VitPro.Engine;
using System;

[Serializable]
class JumpTile : Tile
{
    public override void Effect(Player player, Side side)
    {
        if (side != Side.Down)
            return;
        if (player.States.IsWalking || player.States.IsFlying)
        {
            player.States.Jump();
            player.Velocity = new Vec2(0, player.JumpForce * 2);
        }
    }

    public override void Render()
    {
        Draw.Rect(Position + Size, Position - Size, Color.Green);
    }
}