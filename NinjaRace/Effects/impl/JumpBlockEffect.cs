using VitPro;
using VitPro.Engine;
using System;

class JumpBlockEffect : Effect
{
    Player Player;
    public JumpBlockEffect(Player player)
        : base(player.Position)
    {
        Player = player;
    }
    public override void Update(double dt)
    {
        base.Update(dt);
        Position = Player.Position;
    }
    public override void Render()
    {
        Vec2 v = Position + new Vec2(0, 30) + Vec2.OrtX * Player.Dir * 5;
        RenderState.Push();
        RenderState.Color = Color.Red;
        Draw.Circle(v, 8);
        RenderState.Color = Color.White;
        Draw.Circle(v, 5);
        RenderState.Color = Color.Red;
        Draw.Line(v + new Vec2(4, 4), v - new Vec2(4, 4), 3);
        RenderState.Pop();
    }
}