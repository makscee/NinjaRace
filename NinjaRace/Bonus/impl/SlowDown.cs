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
                Player op = player.GetOpponent();
                op.SpeedUp -= 0.25;
                int lives = op.Lives;
                Effect sd = new SlowDownEffect(op);
                Program.World.EffectsTop.Add(sd);
                Timer t = new Timer(5, () =>
                {
                    op.SpeedUp += 0.25;  
                    Program.World.EffectsTop.Remove(sd);
                });
                op.NextDeath += t.Complete;
                player.Bonus = () => { };
                e.Dispose();
            };
    }
}