using VitPro;
using VitPro.Engine;
using System;

class RocketJumpState : PlayerState
{
    public RocketJumpState(Player player) : base(player) { }

    public override void Render()
    {
        Draw.Rect(player.Position + player.Size, player.Position - player.Size, Color.Orange);
    }
    bool first = true;
    public override void Update(double dt)
    {
        if (!first)
            CheckCols();
        else first = false;
        player.Velocity -= Vec2.Clamp(new Vec2(0, player.Velocity.Y + player.Gravity), player.GAcc * dt);
    }
    public override void Jump()
    {
    }

    void CheckCols()
    {
        if (player.collisions[Side.Left].Count > 0 ||
            player.collisions[Side.Right].Count > 0 ||
            player.collisions[Side.Down].Count > 0)
            player.States.Reset();
    }

    public void Reset()
    {
        first = true;
    }
}