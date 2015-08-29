using System;
using System.Timers;
using VitPro;
using VitPro.Engine;

class SlowDown : Bonus
{
    public override void Get(Player player)
    {
        Effect e = new BonusOnScreen(new Texture("./Data/img/bonuses/slow.png"), player);
        RemoveBonusOnScreen(player);
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