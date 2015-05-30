using System;
using System.Collections.Generic;
using VitPro;
using VitPro.Engine;

class BonusTile : Tile
{
    public BonusTile()
    {
        Mark = true;
    }

    List<Bonus> bonuses = new List<Bonus>() { new FreezeBonus() };

    public override void Render()
    {
        Draw.Rect(Position + Size, Position - Size, Color.Orange);
    }

    public override void Effect(Player player, Side side)
    {
        Program.World.level.tiles.DeleteTile(ID);
        Program.World.EffectsTop.Add(new BonusGet(Position, player));
        bonuses[Program.Random.Next(bonuses.Count)].Get(player);
    }
}