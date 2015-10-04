using System;
using VitPro.Engine;
using VitPro;

class SpeedUp : Bonus
{
    static Texture Tex = new Texture("./Data/img/bonuses/fast.png");
    public override Texture GetTexture()
    {
        return Tex;
    }
    public override void Get(Player player)
    {
        player.SpeedUp += 0.5;
        Effect e = new SpeedUpEffect(player);
        Program.World.EffectsTop.Add(e);
        Timer t = new Timer(5, () => 
        {
            player.SpeedUp -= 0.5;
            Program.World.EffectsTop.Remove(e);
        });
        player.NextDeath += t.Complete;
    }
}