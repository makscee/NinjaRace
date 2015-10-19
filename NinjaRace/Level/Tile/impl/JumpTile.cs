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
        if (side != Side.Down)
            return;
        if (player.States.IsWalking || player.States.IsFlying)
        {
            player.States.Jump();
            player.Velocity = new Vec2(0, player.JumpForce * 2);
        }
        colorBlend = 1;
    }
    double pulse = 0, colorBlend = 0;
    public override void Update(double dt)
    {
        base.Update(dt);
        pulse = pulse > 1.1 ? -1.1 : pulse + dt * 3.5;
        colorBlend = colorBlend < 0 ? 0 : colorBlend - dt;
    }

    protected override void SetAdditionalParameters()
    {
        RenderState.Set("color", new Color(Color.R + colorBlend, Color.G, Color.B, Color.A));
        RenderState.Set("pulse", pulse);
    }
}