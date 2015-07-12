using VitPro;
using VitPro.Engine;
using System;

class Falling : PlayerState
{
    AnimatedTexture tex;
    public bool CanJump;
    public Falling(Player player) : base(player) 
    {
        tex = new AnimatedTexture();
        for (int i = 1; i <= 5; i++)
            tex.Add(new Texture("./Data/img/player/fall/fall" + i + ".png"), 0.05);
    }
    public override void Update(double dt)
    {
        base.Update(dt);
        player.Velocity -= Vec2.Clamp(new Vec2(player.Velocity.X - player.Controller.NeedVel().X * player.Speed * player.SpeedUp, 0), player.Acc * player.SpeedUp * dt);
        player.Velocity -= Vec2.Clamp(new Vec2(0, player.Velocity.Y + player.Gravity), player.GAcc * dt);
    }

    public override AnimatedTexture GetTexture()
    {
        return tex;
    }

    public override void Jump()
    {
        if (CanJump)
        {
            base.Jump();
        }
    }
}