using System;
using VitPro.Engine;
using VitPro;

class SpeedUp : Bonus
{
    public override void Get(Player player)
    {
        player.SpeedUp += 0.5;
        int lives = player.Lives;
        Effect e = new SpeedUpEffect(player);
        Program.World.EffectsTop.Add(e);
        Timer t = new Timer(5, () => 
        {
            if(player.Lives == lives)
                player.SpeedUp -= 0.5;
            Program.World.EffectsTop.Remove(e);
        });
    }
}