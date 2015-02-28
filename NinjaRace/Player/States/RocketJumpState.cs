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
    public override void Update(double dt)
    {
        if (GetTime() > 0.1)
            CheckCols();
        player.Velocity -= Vec2.Clamp(new Vec2(0, player.Velocity.Y + player.Gravity), player.GAcc * dt);
    }
    public override void Jump()
    {
    }

    void CheckCols()
    {
        if (player.TouchWalls())
            player.States.Reset();
    }
}