using System;
using VitPro;
using VitPro.Engine;

[Serializable]
class Ground : Tile
{
    int Sides = 15;
    int Corners = 0;
    public Ground()
    {
        Color = new Color(0.6, 0.8, 0.3);
        Shader = new Shader(NinjaRace.Shaders.GroundTile);
    }
    public override void Update(double dt)
    {
        base.Update(dt);
        Tiles tiles = Program.World.Level.Tiles;
        if (!tiles.Dirty)
            return;
        Vec2i pos = Tiles.GetCoords(ID);
        Sides = 15;
        if (tiles.GetTile(pos.X, pos.Y + 1) is Ground)
            Sides -= 1;
        if (tiles.GetTile(pos.X + 1, pos.Y) is Ground)
            Sides -= 2;
        if (tiles.GetTile(pos.X, pos.Y - 1) is Ground)
            Sides -= 4;
        if (tiles.GetTile(pos.X - 1, pos.Y) is Ground)
            Sides -= 8;
        Corners = 0;
        if ((Sides & 1) == 0 && (Sides & 2) == 0 && !(tiles.GetTile(pos.X + 1, pos.Y + 1) is Ground))
            Corners += 1;
        if ((Sides & 2) == 0 && (Sides & 4) == 0 && !(tiles.GetTile(pos.X + 1, pos.Y - 1) is Ground))
            Corners += 2;
        if ((Sides & 4) == 0 && (Sides & 8) == 0 && !(tiles.GetTile(pos.X - 1, pos.Y - 1) is Ground))
            Corners += 4;
        if ((Sides & 8) == 0 && (Sides & 1) == 0 && !(tiles.GetTile(pos.X - 1, pos.Y + 1) is Ground))
            Corners += 8;
    }
    protected override void SetAdditionalParameters()
    {
        RenderState.Set("sides", Sides);
        RenderState.Set("corners", Corners);
    }
    public override void Effect(Player player, Side side)
    {
        Color = new Color(0.3, 0.5, 1);
    }
}