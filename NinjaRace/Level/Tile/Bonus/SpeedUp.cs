using System;
using VitPro.Engine;
using VitPro;

class SpeedUp : Bonus
{
    public override void Render()
    {
        Draw.Rect(Position + Size, Position - Size, Color.Orange);
    }

    public override void Effect(Player player, Side side)
    {
        base.Effect(player, side);
        player.SpeedUp = 2;
    }
}