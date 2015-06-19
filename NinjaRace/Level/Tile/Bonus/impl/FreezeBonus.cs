using VitPro;
using VitPro.Engine;
using System;
using System.Timers;

class FreezeBonus : Bonus
{
    public void Get(Player player)
    {
        Effect e = new BonusOnScreen(null);
        foreach (var a in Program.World.EffectsScreen)
            if (a is BonusOnScreen)
                a.Dispose();
        Program.World.EffectsScreen.Add(e);
        player.bonus = () => 
        {
            Player op = player.GetOpponent();
            op.States.Set(new Frozen(op));
            Timer t = new Timer(2, () => { op.States.SetFlying(); });
            player.bonus = () => { };
            e.Dispose();
        };
    }
}

class Frozen : PlayerState
{
    public Frozen(Player player)
        : base(player)
    {
        player.Velocity = Vec2.Zero;
    }

    AnimatedTexture tex;

    public override AnimatedTexture GetTexture()
    {
        if (tex == null)
            tex = new AnimatedTexture(new Texture("./Data/img/player/idle/player1.png"));
        return tex;
    }

    public override void Update(double dt) { }

    public override void Jump() { }

    public override void Render()
    {
		RenderState.Push();
		RenderState.Color = new Color(0.5, 0.5, 1);
        RenderState.Translate(player.Position - player.Size);
        RenderState.Scale(player.Size * 2);
        if (player.Dir == -1)
            RenderState.SetOrts(-Vec2.OrtX, Vec2.OrtY, new Vec2(1, 0));
        GetTexture().Render();
		RenderState.Pop();
    }
}