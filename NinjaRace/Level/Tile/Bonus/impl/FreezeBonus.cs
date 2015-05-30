using VitPro;
using VitPro.Engine;
using System;

class FreezeBonus : Bonus
{
    public void Get(Player player)
    {
        player.bonus = () => 
        {
            Program.World.EffectsTop.Add(new Freeze(player.GetOpponent()));
            player.bonus = () => { };
        };
    }
}