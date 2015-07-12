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
            if (a.GetType() == typeof(BonusOnScreen) && ((BonusOnScreen)a).player == player)
                a.Dispose();
        Program.World.EffectsScreen.Add(e);
        player.Bonus = () =>
            {
                player.GetOpponent().SpeedUp -= 0.25;
                int lives = player.GetOpponent().Lives;
                new Timer(5, () =>
                {
                    if (player.Lives == lives)
                        player.SpeedUp += 0.25;
                });
                player.Bonus = () => { };
                e.Dispose();
            };
    }
}