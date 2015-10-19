using System;
using VitPro;
using VitPro.Engine;

[Serializable]
class FinishTile : Tile
{
    public FinishTile()
    {
        Shader = new Shader(NinjaRace.Shaders.FinishTile);
    }
    double r = 0, g = 0, b = 0;
    public override void Update(double dt)
    {
        base.Update(dt);
        r = r > 1.7 ? 0.3 : r + dt;
        g = g > 1.7 ? 0.3 : g + dt * 2.3;
        b = b > 1.7 ? 0.3 : b + dt * 3.7;
    }
    protected override void SetAdditionalParameters()
    {
        RenderState.Set("color", new Color(r > 1 ? 2 - r : r, g > 1 ? 2 - g : g, b > 1 ? 2 - b : b));
    }
    public override void Effect(Player player, Side side)
    {
        if (Program.Manager.CurrentState is Game && !player.States.IsDead)
        {
            ((Game)Program.Manager.CurrentState).Finish(player);
        }
    }
}