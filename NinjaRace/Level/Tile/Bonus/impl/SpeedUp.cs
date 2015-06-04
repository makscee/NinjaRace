using System;
using System.Timers;
using VitPro.Engine;
using VitPro;

class SpeedUp : Bonus
{
    public void Get(Player player)
    {
        player.SpeedUp += 0.5;
        Timer t = new Timer(5000);
        t.Elapsed += new ElapsedEventHandler((Object source, ElapsedEventArgs e) => { player.SpeedUp -= 0.5; });
        t.Enabled = true;
        t.AutoReset = false;
    }
}