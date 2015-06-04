using System;
using System.Timers;
using VitPro;
using VitPro.Engine;

class SlowDown : Bonus
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
                Program.World.EffectsTop.Add(new SlowEffect(player.GetOpponent()));
                player.bonus = () => { };
                e.Dispose();
            };
    }
}

class SlowEffect : Effect
{
    Player player;
    public SlowEffect(Player player)
        : base(player.Position)
    {
        this.player = player;
        SetDuration(5);
        player.SpeedUp -= 0.25;
    }

    public override void Dispose()
    {
        base.Dispose();
        player.SpeedUp += 0.25;
    }
}