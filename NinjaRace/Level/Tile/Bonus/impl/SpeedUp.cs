using System;
using VitPro.Engine;
using VitPro;

class SpeedUp : Bonus
{
    public void Get(Player player)
    {
        player.SpeedUp += 0.5;
        Timer t = new Timer(5, () => { player.SpeedUp -= 0.5; });
    }
}