using VitPro;
using VitPro.Engine;
using System;

class RocketJump : Ability
{
    Vec2 push = new Vec2(900, 300);
    public RocketJump(Player player) : base(player)
    {
        state = new RocketJumpState(player);
    }

    public override void Use()
    {
        ((RocketJumpState)state).Reset();
        player.States.Set(state);
        player.Velocity = new Vec2(player.Dir * push.X, push.Y);
    }
}