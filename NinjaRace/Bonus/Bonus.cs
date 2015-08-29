using VitPro;
using VitPro.Engine;
using System;

abstract class Bonus
{
    public abstract void Get(Player player);
    protected void RemoveBonusOnScreen(Player player)
    {
        foreach (var a in Program.World.EffectsScreen)
            if (a.GetType() == typeof(BonusOnScreen) && ((BonusOnScreen)a).player == player)
                a.Dispose();
    }
}