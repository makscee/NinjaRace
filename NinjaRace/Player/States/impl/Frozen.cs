using VitPro;
using VitPro.Engine;
using System;

class Frozen : PlayerState
{
    public Frozen(Player player) : base(player) 
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