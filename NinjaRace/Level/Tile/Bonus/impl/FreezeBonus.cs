using VitPro;
using VitPro.Engine;
using System;

class FreezeBonus : Bonus
{
    public void Get(Player player)
    {
        Effect e = new BonusOnScreen(null);
        Program.World.EffctsScreen.Add(e);
        player.bonus = () => 
        {
            Program.World.EffectsTop.Add(new Freeze(player.GetOpponent()));
            player.bonus = () => { };
            e.Dispose();
        };
    }
}

class Freeze : Effect
{
    Player player;

    public Freeze(Player player)
        : base(player.Position)
    {
        this.player = player;
        player.States.Set(new Frozen(player));
        Duration = 2;
    }

    public override void Dispose()
    {
        player.States.SetFlying();
        base.Dispose();
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