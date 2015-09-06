using VitPro;
using VitPro.Engine;
using System;

class JumpBlock : Bonus
{
    static Texture Tex = new Texture("./Data/img/bonuses/jumpblock.png");
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
            op.States.CanJump = false;
            Effect jb = new JumpBlockEffect(op);
            Program.World.EffectsTop.Add(jb);
            Timer t = new Timer(3, () =>
            {
                op.States.CanJump = true;
                Program.World.EffectsTop.Remove(jb);
            });
            op.NextDeath += t.Complete;
            player.Bonus = () => { };
            e.Dispose();
        };
    }
}