using VitPro;
using VitPro.Engine;
using System;
using System.Threading;

class FreezeBonus : Bonus
{
    static Timer t;
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
            t = new Timer((Object state) => { op.States.SetFlying(); }, null, 2000, Timeout.Infinite);
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

    public override void Update(double dt) { }

    public override void Jump() { }

    public override void Render()
    {
        Draw.Rect(player.Position + player.Size, player.Position - player.Size, new Color(0.5, 0.5, 1));
    }
}