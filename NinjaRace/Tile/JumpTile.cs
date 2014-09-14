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
        player.States.current.TileJump();
    }

    public override void Render()
    {
        Draw.Rect(Position + Size, Position - Size, Color.Green);
    }

    public override object Clone()
    {
        return new JumpTile().SetPosition(Position);
    }
}