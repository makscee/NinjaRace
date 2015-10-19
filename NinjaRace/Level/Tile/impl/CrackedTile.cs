using VitPro;
using VitPro.Engine;
using System;

class CrackedTile : Tile
{
    Timer Timer;
    public CrackedTile()
    {
        Timer = new Timer(0.5, () => { Program.World.Level.Tiles.DeleteTile(ID); });
        Timer.Stop();
        Shader = new Shader(NinjaRace.Shaders.CrackedTile);
        Color = new Color(0.6, 0.6, 0.1);
    }
    protected override void SetAdditionalParameters()
    {
        RenderState.Set("fade", Timer.Elapsed * 2);
    }
    public override void Effect(Player player, Side side)
    {
        Timer.Start();
    }
}