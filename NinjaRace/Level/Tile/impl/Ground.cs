using System;
using VitPro;
using VitPro.Engine;

[Serializable]
class Ground : Tile
{
    int Sides = 15;
    public Ground()
    {
        Color = new Color(0.6, 0.8, 0.3);
        Shader = new Shader(NinjaRace.Shaders.TileBorder);
    }
    protected override void LoadTexture()
    {
        tex = new AnimatedTexture(new Texture("./Data/img/tiles/ground.png"));
    }
    public override void Update(double dt)
    {
        base.Update(dt);
        Sides = 15;
        Vec2i pos = Tiles.GetCoords(ID);
        Tiles tiles = Program.World.Level.Tiles;
        if (pos.Y != tiles.GetLength(0) && tiles.GetTile(pos.X, pos.Y + 1) is Ground)
            Sides -= 1;
        if (pos.X != tiles.GetLength(1) && tiles.GetTile(pos.X + 1, pos.Y) is Ground)
            Sides -= 2;
        if (pos.Y > 1 && tiles.GetTile(pos.X, pos.Y - 1) is Ground)
            Sides -= 4;
        if (pos.X > 1 && tiles.GetTile(pos.X - 1, pos.Y) is Ground)
            Sides -= 8;
    }
    protected override void SetAdditionalParameters()
    {
        RenderState.Set("sides", Sides);
    }
    public override void Effect(Player player, Side side)
    {
        Color = new Color(0.3, 0.5, 1);
    }
}