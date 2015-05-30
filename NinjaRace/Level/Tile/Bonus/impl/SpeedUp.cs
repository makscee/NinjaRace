using System;
using VitPro.Engine;
using VitPro;

class SpeedUp : Bonus
{
    public void Get(Player player)
    {
        player.SpeedUp = 1.5;
    }
}