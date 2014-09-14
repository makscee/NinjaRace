using VitPro;
using VitPro.Engine;
using System;

[Serializable]
class Spikes : Tile
{
    public override void Render()
    {
        Draw.Rect(Position + Size, Position - Size, Color.Gray);
    }

    public override void Effect(Player player, Side side)
    {
        player.States.current.Die(Position);
    }

    public override object Clone()
    {
        return new Spikes();
    }
}