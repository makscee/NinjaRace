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
            Timer t = new Timer(2000);
            t.Elapsed += new ElapsedEventHandler((Object source, ElapsedEventArgs ee) => { op.States.SetFlying(); });
            t.Enabled = true;
            t.AutoReset = false;
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