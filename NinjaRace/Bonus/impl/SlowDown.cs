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
                Timer t = new Timer(5, () =>
                {
                    player.GetOpponent().SpeedUp += 0.25;
                });
                player.NextDeath += t.Complete;
                player.Bonus = () => { };
                e.Dispose();
            };
    }
}