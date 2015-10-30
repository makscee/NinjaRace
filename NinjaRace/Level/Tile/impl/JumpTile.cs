using VitPro;
using VitPro.Engine;
using System;

[Serializable]
class JumpTile : Tile
{
    public JumpTile()
    {
        Color = new Color(0, 0.7, 0.2);
        Shader = new Shader(NinjaRace.Shaders.JumpTile);
    }
    public override void Effect(Player player, Side side)
    {
        if (side != Side.Down || player.States.IsDead)
            return;
        if (player.States.IsWalking || player.States.IsFlying)
        {
            player.States.Jump();
            player.Velocity = new Vec2(0, player.JumpForce * 2);
        }
        ColorBlend = 1;
        Program.World.EffectsMid.Add(new JumpTileEffect(Position));
    }
    double Pulse = 0, ColorBlend = 0;
    public override void Update(double dt)
    {
        base.Update(dt);
        Pulse = Pulse > 1.1 ? -1.1 : Pulse + dt * 3.5;
        ColorBlend = ColorBlend < 0 ? 0 : ColorBlend - dt;
    }

    protected override void SetAdditionalParameters()
    {
        RenderState.Set("color", new Color(Color.R + ColorBlend, Color.G, Color.B, Color.A));
        RenderState.Set("pulse", Pulse);
    }
}