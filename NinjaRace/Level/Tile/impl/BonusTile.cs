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

    List<Bonus> bonuses = new List<Bonus>() { new SpeedUp(), new FreezeBonus(), new SlowDown() };
    public override void Effect(Player player, Side side)
    {
        Program.World.level.Tiles.DeleteTile(ID);
        new Timer(4, () => { Program.World.level.Tiles.AddTile(Tiles.GetCoords(ID), new BonusTile()); });
        Program.World.EffectsTop.Add(new BonusGet(Position, player));
        bonuses[Program.Random.Next(bonuses.Count)].Get(player);
    }

    protected override void LoadTexture()
    {
        tex = new AnimatedTexture(new Texture("./Data/img/tiles/bonus.png"));
    }
}