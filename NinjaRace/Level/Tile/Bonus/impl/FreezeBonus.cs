using VitPro;
using VitPro.Engine;
using System;

class FreezeBonus : Bonus
{
    public void Get(Player player)
    {
        Effect e = new BonusOnScreen(null);
        Program.World.EffctsScreen.Add(e);
        player.bonus = () => 
        {
            Program.World.EffectsTop.Add(new Freeze(player.GetOpponent()));
            player.bonus = () => { };
            e.Dispose();
        };
    }
}