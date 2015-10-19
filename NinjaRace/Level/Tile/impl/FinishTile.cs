using System;
using VitPro;
using VitPro.Engine;

[Serializable]
class FinishTile : Tile
{
    public FinishTile()
    {
        Shader = new Shader(NinjaRace.Shaders.FinishTile);
        Color = new Color(0.38, 0.15, 1);
    }
    public override void Effect(Player player, Side side)
    {
        if (Program.Manager.CurrentState is Game && !player.States.IsDead)
        {
            ((Game)Program.Manager.CurrentState).Finish(player);
        }
    }
}