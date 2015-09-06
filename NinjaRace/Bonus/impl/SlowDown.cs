using System;
using VitPro;
using VitPro.Engine;

class SlowDown : Bonus
{
    static Texture Tex = new Texture("./Data/img/bonuses/slow.png");
    public override Texture GetTexture()
    {
        return Tex;
    }
    public override void Get(Player player)
    {
        Effect e = new BonusOnScreen(Tex.Copy(), player);
        RemoveBonusOnScreen(player);
        Program.World.EffectsScreen.Add(e);
        player.Bonus = () =>
            {
                Player op = player.GetOpponent();
                op.SpeedUp -= 0.25;
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