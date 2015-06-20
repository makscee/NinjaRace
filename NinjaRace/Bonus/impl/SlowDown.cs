using System;
using System.Timers;
using VitPro;
using VitPro.Engine;

class SlowDown : Bonus
{
    public void Get(Player player)
    {
        Effect e = new BonusOnScreen(new Texture("./Data/img/bonuses/slow.png"), player);
        foreach (var a in Program.World.EffectsScreen)
            if (a is BonusOnScreen && ((BonusOnScreen)a).player == player)
                a.Dispose();
        Program.World.EffectsScreen.Add(e);
        player.bonus = () =>
            {
                player.GetOpponent().SpeedUp -= 0.25;
                int lives = player.GetOpponent().lives;
                new Timer(5, () =>
                {
                    if (player.lives == lives)
                        player.SpeedUp += 0.25;
                });
                player.bonus = () => { };
                e.Dispose();
            };
    }
}