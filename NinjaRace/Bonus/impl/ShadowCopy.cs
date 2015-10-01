using VitPro;
using VitPro.Engine;
using System;

class ShadowCopy : Bonus
{
    static Texture Tex = new Texture("./Data/img/bonuses/copy.png");
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
            Program.World.EffectsTop.Add(new SmokeExplosionEffect(player.Position));
            Program.Manager.PushState(new CopyChose(player));
            player.Bonus = () => { };
            e.Dispose();
        };
    }
}