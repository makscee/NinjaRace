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
    Button done;
    public LevelEditor(int sizex, int sizey)
    {
        tiles = new Tiles(sizex, sizey);
        currentTile = new Ground();
        cam.Position = new Vec2(100, 100);
        done = new Button(new Vec2(130, -110), new Vec2(25, 8))
            .SetName("DONE")
            .SetTextScale(12)
            .SetAction(() => { GUtil.Dump(tiles, "./level.dat"); this.Close(); });
    }

    public LevelEditor()
    {
        tiles = GUtil.Load<Tiles>("./level.dat");
        currentTile = new Ground();
        cam.Position = new Vec2(100, 100);
        done = new Button(new Vec2(130, -110), new Vec2(25, 8))
            .SetName("DONE")
            .SetTextScale(12)
            .SetAction(() => { GUtil.Dump(tiles, "./level.dat"); this.Close(); });
    }

    public override void MouseDown(MouseButton button, Vec2 pos)
    {
        if (button == MouseButton.Right)
        {
            dragging = true;
            draggingVec = Program.MousePosition() * cam.FOV / 240;
        }
        if (button == MouseButton.Left)
        {
            tiles.AddTile(Geti(), Getj(), currentTile);
            done.Click();
        }
    }

    int Geti()
    {
        double t = Program.MousePosition().Y * cam.FOV / 240 + cam.Position.Y;
        return (int)Math.Round(t / Tile.Size.Y / 2);
    }

    int Getj()
    {
        double t = Program.MousePosition().X * cam.FOV / 240 + cam.Position.X;
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
        if (key == Key.Number4)
            currentTile = new JumpTile();
        if (key == Key.Number5)
            currentTile = new StartTile();
        if (key == Key.Number6)
            currentTile = new FinishTile();

        if (key == Key.W)
            cam.FOV /= 1.2;
        if (key == Key.S)
            cam.FOV *= 1.2;
    }

    public override void MouseWheel(double delta)
    {
        base.MouseWheel(delta);
        cam.FOV /= 1 + delta * 0.2;
    }

    public override void Update(double dt)
    {
        if (dragging)
        {
            cam.Position += draggingVec - Program.MousePosition() * cam.FOV / 240;
            draggingVec = Program.MousePosition() * cam.FOV / 240;
        }
    }

    void RenderTiles()
    {
        for (int i = 1; i < tiles.GetLength(0); i++)
            for (int j = 1; j < tiles.GetLength(1); j++)
            {
                if (tiles.GetTile(i, j) == null)
                    Draw.Rect(new Vec2(j * Tile.Size.X * 2, i * Tile.Size.Y * 2) - Tile.Size * 0.9
                        , new Vec2(j * Tile.Size.X * 2, i * Tile.Size.Y * 2) + Tile.Size * 0.9, new Color(0.1, 0.1, 0.1));
                else if (tiles.GetTile(i, j) is StartTile)
                    Draw.Rect(new Vec2(j * Tile.Size.X * 2, i * Tile.Size.Y * 2) - Tile.Size * 0.9
                        , new Vec2(j * Tile.Size.X * 2, i * Tile.Size.Y * 2) + Tile.Size * 0.9, new Color(0, 0.2, 0));
            }
        tiles.Render();
    }

    public override void Render()
    {
        cam.Apply();
        Draw.Clear(Color.Black);
        RenderTiles();
        new Camera(240).Apply();
        done.Render();
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

        v = new Vec2(-Tile.Size.X * 1.1, -Tile.Size.Y * 1.1 * 7);
        if (currentTile is JumpTile)
            Draw.Rect(v + Tile.Size * 1.1, v - Tile.Size * 1.1, Color.Orange);
        new JumpTile().SetPosition(v).Render();

        v = new Vec2(-Tile.Size.X * 1.1, -Tile.Size.Y * 1.1 * 9);
        if (currentTile is StartTile)
            Draw.Rect(v + Tile.Size * 1.1, v - Tile.Size * 1.1, Color.Orange);
        Draw.Rect(v + Tile.Size, v - Tile.Size, new Color(0, 0.2, 0));

        v = new Vec2(-Tile.Size.X * 1.1, -Tile.Size.Y * 1.1 * 11);
        if (currentTile is FinishTile)
            Draw.Rect(v + Tile.Size * 1.1, v - Tile.Size * 1.1, Color.Orange);
        new FinishTile().SetPosition(v).Render();

        Draw.Load();
    }
}