using VitPro;
using VitPro.Engine;
using System;

class Spikes : Tile
{
    public override void Render()
    {
        Draw.Rect(Position + Size, Position - Size, Color.Gray);
    }

    public override void Effect(Player player)
    {
        player.State.Die(Position);
    }

    public override object Clone()
    {
        return new Spikes();
    }
}