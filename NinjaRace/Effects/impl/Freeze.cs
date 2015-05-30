using VitPro;
using VitPro.Engine;
using System;

class Freeze : Effect
{
    Player player;

    public Freeze(Player player) : base(player.Position)
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