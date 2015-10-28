using VitPro;
using VitPro.Engine;
using System;

class BorderTile : Tile
{
    int Side = -1;
    public BorderTile()
    {
        Color = new Color(0.6, 0.8, 0.3);
        Shader = new Shader(NinjaRace.Shaders.BorderTile);
    }
    public override void Update(double dt)
    {
        base.Update(dt);
        if (Side != -1)
            return;
        if ((Tiles.GetCoords(ID).X == 0 || Tiles.GetCoords(ID).X == Program.World.Level.Tiles.GetLength(1)) &&
            (Tiles.GetCoords(ID).Y == 0 || Tiles.GetCoords(ID).Y == Program.World.Level.Tiles.GetLength(0)))
        {
            Shader = new Shader(NinjaRace.Shaders.BorderTileCorner);
            if (Tiles.GetCoords(ID).Y == 0)
                Side = Tiles.GetCoords(ID).X == 0 ? 0 : 3;
            else
                Side = Tiles.GetCoords(ID).X == 0 ? 1 : 2;
            return;
        }
        if (Tiles.GetCoords(ID).X == 0)
        {
            Side = 1;
            return;
        }
        if (Tiles.GetCoords(ID).Y == 0)
        {
            Side = 0;
            return;
        }
        if (Tiles.GetCoords(ID).X == Program.World.Level.Tiles.GetLength(1))
        {
            Side = 3;
            return;
        }
        Side = 2;
    }
    protected override void SetAdditionalParameters()
    {
        RenderState.Set("side", Side);
    }
    public override void Effect(Player player, Side side)
    {
        Color = new Color(0.3, 0.5, 1);
    }
}