using VitPro;
using VitPro.Engine;
using System;

[Serializable]
class JumpTile : Tile
{
    Color Color = new Color(0, 0.7, 0.2);
    Shader Shader = new Shader(NinjaRace.Shaders.JumpTile);
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
    double t = 0, pulse = 0, colorBlend = 0;
    public override void Update(double dt)
    {
        t += dt;
        pulse = pulse > 2 ? 0 : pulse + dt * 2;
        colorBlend = colorBlend < 0 ? 0 : colorBlend - dt;
    }

    public override void Render()
    {
        RenderState.Push();
        RenderState.Translate(Position);
        RenderState.Scale(Tile.Size * 2);
        RenderState.Origin(0.5, 0.5);
        RenderState.Set("color", new Color(Color.R + colorBlend, Color.G, Color.B, Color.A));
        RenderState.Set("size", Math.Sin(t) + 1);
        RenderState.Set("pulse", pulse);
        Shader.RenderQuad();
        RenderState.Pop();
    }
}