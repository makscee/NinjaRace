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
        currentTile = new Ground();
        cam.Position = new Vec2(100, 100);
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
            tiles.AddTile(Geti(), Getj(), currentTile);
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

    public override void KeyDown(Key key)
    {
        if (key == Key.Number1)
            currentTile = null;
        if (key == Key.Number2)
            currentTile = new Ground();
        if (key == Key.Number3)
            currentTile = new Spikes();
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
        RenderTileMenu();
    }

    void RenderTileMenu()
    {
        Vec2 v = new Vec2(-Tile.Size.X * 1.1, -Tile.Size.Y * 1.1);
        Draw.Save();

        new Camera(360).Apply();
        Draw.Translate(new Vec2(240, 180));

        if (currentTile == null)
            Draw.Rect(v + Tile.Size * 1.1, v - Tile.Size * 1.1, Color.Orange);

        v = new Vec2(-Tile.Size.X * 1.1, -Tile.Size.Y * 1.1 * 3);
        if (currentTile is Ground)
            Draw.Rect(v + Tile.Size * 1.1, v - Tile.Size * 1.1, Color.Orange);
        new Ground().SetPosition(v).Render();

        v = new Vec2(-Tile.Size.X * 1.1, -Tile.Size.Y * 1.1 * 5);
        if (currentTile is Spikes)
            Draw.Rect(v + Tile.Size * 1.1, v - Tile.Size * 1.1, Color.Orange);
        new Spikes().SetPosition(v).Render();

        Draw.Load();
    }
}