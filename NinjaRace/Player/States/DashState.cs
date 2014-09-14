﻿using VitPro;
using VitPro.Engine;
using System;

class DashState : PlayerState
{
    public Vec2 dir = Vec2.Zero;
    double speed;
    public DashState(Player player) : base(player) 
    {
        speed = player.Speed * 6;
    }

    public override void Render()
    {
        Draw.Rect(player.Position + player.Size, player.Position - player.Size, Color.Magenta);
    }
    public override void Update(double dt)
    {
        player.Velocity = dir * speed;
        if (dir.X != 0 && (player.collisions[Side.Left].Count > 0 || player.collisions[Side.Right].Count > 0) ||
            dir.Y != 0 && player.collisions[Side.Down].Count > 0)
            player.States.Reset();
    }

    public override void TileJump() { }

    public override void Die(Vec2 position) { }

    public override void Jump() { }
}