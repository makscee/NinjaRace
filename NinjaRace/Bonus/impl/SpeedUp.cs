using System;
using VitPro.Engine;
using VitPro;

class SpeedUp : Bonus
{
    public void Get(Player player)
    {
        player.SpeedUp += 0.5;
        int lives = player.lives;
        Timer t = new Timer(5, () => 
        {
            if(player.lives == lives)
                player.SpeedUp -= 0.5;
        });
    }
}