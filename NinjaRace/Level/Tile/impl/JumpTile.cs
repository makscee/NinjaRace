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
        if (player.States.current is Walking || player.States.current is Flying)
        {
            player.States.SetFlying();
            player.Velocity = new Vec2(0, player.JumpForce * 2);
        }
    }

    public override void Render()
    {
        Draw.Rect(Position + Size, Position - Size, Color.Green);
    }
}