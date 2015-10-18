using System;
using VitPro;
using VitPro.Engine;

[Serializable]
class Ground : Tile
{
    Shader Shader = new Shader(NinjaRace.Shaders.TileBorder);
    double t = 0;
    int Sides = -1;
    Color Color = new Color(0.6, 0.8, 0.3);
    public Ground()
    {
        Colorable = true;
    }
    protected override void LoadTexture()
    {
        tex = new AnimatedTexture(new Texture("./Data/img/tiles/ground.png"));
    }
    public override void Update(double dt)
    {
        t += dt;
        if (Sides == -1)
        {
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
    }
    public override void Render()
    {
        RenderState.Push();
        RenderState.Translate(Position);
        RenderState.Scale(Tile.Size * 2);
        RenderState.Origin(0.5, 0.5);
        RenderState.Set("color", Color);
        RenderState.Set("size", Math.Sin(t) + 1);
        RenderState.Set("sides", Sides);
        Shader.RenderQuad();
        RenderState.Pop();
    }
    public override void Effect(Player player, Side side)
    {
        Color = new Color(0.3, 0.5, 1);
    }
}