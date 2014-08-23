using VitPro;
using VitPro.Engine;
using System;

class LevelEditor : State
{
    Tiles tiles;
    Tile currentTile;
    Camera cam = new Camera(240);
    bool dragging = false;
    Vec2 draggingVec;
    public LevelEditor(int sizex, int sizey)
    {
        tiles = new Tiles(sizex, sizey);
        currentTile = new Tile();
    }

    public override void MouseDown(MouseButton button, Vec2 pos)
    {
        if (button == MouseButton.Right)
        {
            dragging = true;
            draggingVec = Mouse.Position;
        }
        if (button == MouseButton.Left)
        {
            tiles.AddTileCustom(Geti(), Getj(), currentTile);
        }
    }

    int Geti()
    {
        double t = Program.MousePosition().Y + cam.Position.Y;
        return (int)Math.Round(t / Tile.Size.Y / 2);
    }

    int Getj()
    {
        double t = Program.MousePosition().X + cam.Position.X;
        return (int)Math.Round(t / Tile.Size.X / 2);
    }

    public override void MouseUp(MouseButton button, Vec2 pos)
    {

        if (button == MouseButton.Right)
            dragging = false;
    }

    public override void Update(double dt)
    {
        if (dragging)
        {
            cam.Position += (draggingVec - Mouse.Position) / 2;
            draggingVec = Mouse.Position;
        }
    }

    void RenderTiles()
    {
        for (int i = 1; i < tiles.GetLength(0); i++)
            for (int j = 1; j < tiles.GetLength(1); j++)
                if (tiles.GetTile(i, j) == null)
                    Draw.Rect(new Vec2(j * Tile.Size.X * 2, i * Tile.Size.Y * 2) - Tile.Size * 0.9
                        , new Vec2(j * Tile.Size.X * 2, i * Tile.Size.Y * 2) + Tile.Size * 0.9, new Color(0.2, 0.2, 0.2));
        tiles.Render();
    }

    public override void Render()
    {
        cam.Apply();
        Draw.Clear(Color.Black);
        RenderTiles();
    }
}