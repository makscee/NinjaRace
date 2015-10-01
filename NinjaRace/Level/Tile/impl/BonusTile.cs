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

    List<Bonus> GameBonuses = new List<Bonus>() { new SpeedUp(), new FreezeBonus(), new SlowDown(),
        new JumpBlock(), new Missle() };
    List<Bonus> ShowdownBonuses = new List<Bonus>() { new SpeedUp(), new FreezeBonus(), new SlowDown(),
        new JumpBlock(), new Missle(), new ShadowCopy() };
    public override void Effect(Player player, Side side)
    {
        if (Program.IsCopy(player))
            return;
        Program.Statistics.Bonuses[Program.WhichPlayer(player)]++;
        Program.World.Level.Tiles.DeleteTile(ID);
        new Timer(4, () => { Program.World.Level.Tiles.AddTile(Tiles.GetCoords(ID), new BonusTile()); });
        Bonus b = Program.Manager.CurrentState is Game ?
            GameBonuses[Program.Random.Next(GameBonuses.Count)] :
            ShowdownBonuses[Program.Random.Next(ShowdownBonuses.Count)];
        Program.World.EffectsTop.Add(new BonusGet(Position, b));
        b.Get(player);
    }

    protected override void LoadTexture()
    {
        tex = new AnimatedTexture(new Texture("./Data/img/tiles/bonus.png"));
    }
}