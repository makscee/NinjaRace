using VitPro;
using VitPro.Engine;
using System;

class Dash : Ability
{
    public Dash(Player player)
        : base(player)
    {
        state = new DashState(player);
    }

    public override void Use()
    {
        ((DashState)state).dir = player.States.current is Flying ? new Vec2(0, -1) : new Vec2(player.Dir, 0);
        player.States.Set(state);
    }
}