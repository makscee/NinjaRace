using VitPro;
using VitPro.Engine;
using System;

class Missle : Bonus
{
    static Texture Tex = new Texture("./Data/img/bonuses/missle.png");
    public override Texture GetTexture()
    {
        return Tex;
    }
    public override void Get(Player player)
    {
        RemoveBonusOnScreen(player);
        Program.World.EffectsScreen.Add(new BonusOnScreen(Tex.Copy(), player));
        player.Bonus = () =>
        {
            MissleEffect m = new MissleEffect(player);
            Program.World.EffectsTop.Add(m);
            Program.World.CurrentMissle = m;
            player.Bonus = () => { };
            RemoveBonusOnScreen(player);
        };
    }
}