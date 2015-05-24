using VitPro;
using VitPro.Engine;
using System;
using System.Collections.Generic;

class LevelEditor : State
{
    Level level;
    Tile currentTile;
    Camera cam = new Camera(240);
    bool dragging = false;
    Vec2 draggingVec;
    Button done;
    int vecForSaw1 = -1, vecForSaw2 = -1;

    List<string> TileTypes = new List<string> { "Ground", "Spikes", "JumpTile", "StartTile",
        "FinishTile", "Saw", "SpeedUp" };
    List<string>.Enumerator TTenum;

    public LevelEditor(int sizex, int sizey, string name)
    {
        TTenum = TileTypes.GetEnumerator();
        level = new Level(new Tiles(sizex, sizey), name);
        cam.Position = new Vec2(100, 100);
        done = new Button(new Vec2(130, -110), new Vec2(25, 8))
            .SetName("DONE")
            .SetTextScale(12)
            .SetAction(() => { DBUtils.StoreTiles(level); this.Close(); });
    }

    public LevelEditor(string name)
    {
        TTenum = TileTypes.GetEnumerator();
        level = DBUtils.GetLevel(name);
        cam.Position = new Vec2(100, 100);
        done = new Button(new Vec2(130, -110), new Vec2(25, 8))
            .SetName("DONE")
            .SetTextScale(12)
            .SetAction(() => { DBUtils.StoreTiles(level); this.Close(); });
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
            done.Click();
            if (currentTile is Saw)
            {
                vecForSaw1 = Tiles.GetID(GetX(), GetY());
            }
        }
    }

    int GetY()
    {
        double t = Program.MousePosition().Y * cam.FOV / 240 + cam.Position.Y;
        return (int)Math.Round(t / Tile.Size.Y / 2);
    }

    int GetX()
    {
        double t = Program.MousePosition().X * cam.FOV / 240 + cam.Position.X;
        return (int)Math.Round(t / Tile.Size.X / 2);
    }

    public override void MouseUp(MouseButton button, Vec2 pos)
    {

        if (button == MouseButton.Right)
            dragging = false;
        if (button == MouseButton.Left)
        {
            if (currentTile is Saw)
            {
                vecForSaw2 = Tiles.GetID(GetX(), GetY());
                Saw s = new Saw();
                s.Link = vecForSaw2;
                level.tiles.AddTile(Tiles.GetCoords(vecForSaw1), s);
                vecForSaw1 = vecForSaw2 = -1;
            }
        }
    }

    public override void KeyDown(Key key)
    {
        if (key == Key.E)
        {
            if (TTenum.MoveNext())
            {
                Type t = Type.GetType(TTenum.Current);
                currentTile = (Tile)t.GetConstructor(new Type[] { }).Invoke(new object[] { });
            }
            else
            {
                currentTile = null;
                TTenum = TileTypes.GetEnumerator();
            }
        }
        if (key == Key.Q)
        {
            int t = TileTypes.IndexOf(TTenum.Current);
            if (t == 0)
            {
                currentTile = null;
                TTenum = TileTypes.GetEnumerator();
                return;
            }
            t = t == -1 ? TileTypes.Count : t;
            TTenum = TileTypes.GetEnumerator();
            for (int i = 0; i < t; i++)
                TTenum.MoveNext();
            currentTile = (Tile)Type.GetType(TTenum.Current).GetConstructor(new Type[] { }).Invoke(new object[] { });
        }
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
        if(MouseButton.Left.Pressed() && !(currentTile is Saw))
            level.tiles.AddTile(GetX(), GetY(), currentTile);
        level.Update(dt);
    }

    void RenderTiles()
    {
        for (int y = 1; y < level.tiles.GetLength(0); y++)
            for (int x = 1; x < level.tiles.GetLength(1); x++)
            {
                if (level.tiles.GetTile(x, y) == null)
                    Draw.Rect(new Vec2(x * Tile.Size.X * 2, y * Tile.Size.Y * 2) - Tile.Size * 0.9
                        , new Vec2(x * Tile.Size.X * 2, y * Tile.Size.Y * 2) + Tile.Size * 0.9, new Color(0.1, 0.1, 0.1));
                else if (level.tiles.GetTile(x, y) is StartTile)
                    Draw.Rect(new Vec2(x * Tile.Size.X * 2, y * Tile.Size.Y * 2) - Tile.Size * 0.9
                        , new Vec2(x * Tile.Size.X * 2, y * Tile.Size.Y * 2) + Tile.Size * 0.9, new Color(0, 0.2, 0));
            }
        level.Render();
    }

    public override void Render()
    {
        cam.Apply();
        Draw.Clear(Color.Black);
        RenderTiles();
        if (vecForSaw1 != -1)
            Draw.Rect(Tiles.GetPosition(Tiles.GetCoords(vecForSaw1)) + Tile.Size,
                Tiles.GetPosition(Tiles.GetCoords(vecForSaw1)) - Tile.Size, Color.Green);
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
        for (int i = 0; i < TileTypes.Count; i++)
        {
            v += new Vec2(0, -Tile.Size.Y * 1.1 * 2);
            if (currentTile != null && currentTile.GetType() == Type.GetType(TileTypes[i]))
                Draw.Rect(v + Tile.Size * 1.1, v - Tile.Size * 1.1, Color.Orange);
            ((Tile)Type.GetType(TileTypes[i]).GetConstructor(new Type[] { }).Invoke(new object[] { })).SetPosition(v).Render();
        }
        Draw.Load();
    }
}