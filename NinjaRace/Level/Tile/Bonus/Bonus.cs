using System;
using VitPro;
using VitPro.Engine;

class Bonus : Tile
{
    public Bonus()
    {
        Mark = true;
    }

    public override void Render()
    {
        Draw.Rect(Position + Size, Position - Size, Color.Orange);
    }

    public override void Effect(Player player, Side side)
    {
        player.SpeedUp = 2;
        Program.World.level.tiles.DeleteTile(ID);
    }
}