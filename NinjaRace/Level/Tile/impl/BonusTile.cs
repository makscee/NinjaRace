using System;
using System.Collections.Generic;
using VitPro;
using VitPro.Engine;

class BonusTile : Tile
{
    public BonusTile()
    {
        Mark = true;
        Shader = new Shader(NinjaRace.Shaders.BonusTile);
        r = Program.Random.NextDouble();
        g = Program.Random.NextDouble();
        b = Program.Random.NextDouble();
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

    double r, g, b;
    public override void Update(double dt)
    {
        base.Update(dt);
        r = r > 1.7 ? 0.3 : r + dt;
        g = g > 1.7 ? 0.3 : g + dt * 2.3;
        b = b > 1.7 ? 0.3 : b + dt * 3.7;
    }
    protected override void SetAdditionalParameters()
    {
        RenderState.Set("color", new Color(r > 1 ? 2 - r : r, g > 1 ? 2 - g : g, b > 1 ? 2 - b : b));
    }
}