﻿using VitPro;
using VitPro.Engine;
using UI = VitPro.Engine.UI;
using System;
using System.Collections.Generic;

class LevelEditor : UI.State
{
    Level level;
    Tile currentTile;
    View cam = new View(240);
    bool dragging = false;
    Vec2 draggingVec;
    int vecForSaw1 = -1, vecForSaw2 = -1;
    CheckBox mirror = new CheckBox(20);
    Button done, clear;

    List<string> TileTypes = new List<string> { "Ground", "Spikes", "JumpTile", "StartTile",
        "FinishTile", "Saw", "BonusTile", "CrackedTile" };
    List<string>.Enumerator TTenum;

    bool showdown;
    void init()
    {
        TTenum = TileTypes.GetEnumerator();
        cam.Position = new Vec2(100, 100);
        done = new Button("DONE", () => { level.Name += showdown ? "_S" : ""; DBUtils.StoreLevel(level); this.Close(); }, 20, 50);
        done.Anchor = new Vec2(0.95, 0.05);
        clear = new Button("CLEAR", () => 
        {
            level = new Level(new Tiles(level.Tiles.GetLength(1), level.Tiles.GetLength(0)), level.Name);
            RefreshTexture();
        }, 20, 60);
        clear.Anchor = new Vec2(0.80, 0.05);
        mirror.Anchor = new Vec2(0.1, 0.95);
        mirror.Checked = true;
        Frame.Add(done);
        Frame.Add(mirror);
        Frame.Add(clear);
        RefreshTexture();
    }

    public LevelEditor(int sizex, int sizey, string name, bool showdown)
    {
        level = new Level(new Tiles(sizex, sizey), name);
        init();
        this.showdown = showdown;
    }

    public LevelEditor(string name, bool showdown)
    {
        name = name.Trim();
        level = DBUtils.GetLevel(name + (showdown ? "_S" : ""));
        init();
        showdown = false;
    }

    public override void MouseDown(MouseButton button, Vec2 pos)
    {
        base.MouseDown(button, pos);
        if (button == MouseButton.Right)
        {
            dragging = true;
            draggingVec = cam.FromWH(Mouse.Position, App.Width, App.Height);
        }
        if (button == MouseButton.Left)
        {
            if (currentTile != null && currentTile.GetType() == typeof(Saw))
            {
                vecForSaw1 = Tiles.GetID(GetX(), GetY());
            }
        }
    }

    int GetY()
    {
        double t = cam.FromWH(Mouse.Position, App.Width, App.Height).Y;
        return (int)Math.Round(t / Tile.Size.Y / 2);
    }

    int GetX()
    {
        double t = cam.FromWH(Mouse.Position, App.Width, App.Height).X;
        return (int)Math.Round(t / Tile.Size.X / 2);
    }

    public override void MouseUp(MouseButton button, Vec2 pos)
    {
        base.MouseUp(button, pos);
        if (button == MouseButton.Right)
            dragging = false;
        if (button == MouseButton.Left)
        {
            if (currentTile != null && currentTile.GetType() == typeof(Saw))
            {
                int x1 = Tiles.GetCoords(vecForSaw1).X, x2 = GetX(),
                    y1 = Tiles.GetCoords(vecForSaw1).Y, y2 = GetY();
                vecForSaw2 = Tiles.GetID(x2, y2);
                Saw s = new Saw();
                s.Link = vecForSaw2;
                level.Tiles.AddTile(new Vec2i(x1, y1), s);

                if (mirror.Checked)
                {
                    s = new Saw();
                    s.Link = Tiles.GetID(level.Tiles.GetLength(1) - x2, y2);
                    level.Tiles.AddTile(new Vec2i(level.Tiles.GetLength(1) - x1, y1), s);
                }

                RefreshTexture();
                vecForSaw1 = vecForSaw2 = -1;
            }
        }
    }

    public override void KeyDown(Key key)
    {
        base.KeyDown(key);
        if (key == Key.Escape)
            Close();
        if (key == Key.Enter)
        {
            DBUtils.StoreLevel(level);
            Close();
        }
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
        base.Update(dt);
        if (dragging)
        {
            cam.Position += draggingVec - cam.FromWH(Mouse.Position, App.Width, App.Height);
            draggingVec = cam.FromWH(Mouse.Position, App.Width, App.Height);
        }
        //level.Update(dt);
        if (MouseButton.Left.Pressed() && !(currentTile != null && currentTile.GetType() == typeof(Saw)))
        {
            if (done.Hovered || mirror.Hovered)
                return;
            int x = GetX(), y = GetY();
            Tile t = level.Tiles.GetTile(x, y);
            bool same = false;
            if (currentTile == null && t == null)
                if (mirror.Checked)
                    same = true;
                else return;
            if (currentTile != null && t != null && currentTile.GetType() == t.GetType())
                if (mirror.Checked)
                    same = true;
                else return;
            if (!same)
            {
                level.Tiles.DeleteTile(Tiles.GetID(x, y));
                if (currentTile != null)
                    level.Tiles.AddTile(x, y, currentTile);
            }
            if (mirror.Checked)
            {
                int x2 = level.Tiles.GetLength(1) - x;
                int y2 = y;
                Tile t2 = level.Tiles.GetTile(x2, y2);
                if (currentTile == null && t2 == null && same)
                    return;
                if (currentTile != null &&
                    t2 != null && currentTile.GetType() == t2.GetType() && same)
                    return;
                level.Tiles.DeleteTile(Tiles.GetID(x2, y2));
                if (currentTile != null)
                    level.Tiles.AddTile(x2, y2, currentTile);
            }
            RefreshTexture();
        }
    }

    void RenderTiles()
    {
        for (int y = 1; y < level.Tiles.GetLength(0); y++)
            for (int x = 1; x < level.Tiles.GetLength(1); x++)
            {
				if (level.Tiles.GetTile(x, y) == null) {
					RenderState.Push();
					RenderState.Color = new Color(0.1, 0.1, 0.1);
                    Draw.Rect(new Vec2(x * Tile.Size.X * 2, y * Tile.Size.Y * 2) - Tile.Size * 0.9
                        , new Vec2(x * Tile.Size.X * 2, y * Tile.Size.Y * 2) + Tile.Size * 0.9);
					RenderState.Pop();
				}
                else if (level.Tiles.GetTile(x, y) != null &&
                    level.Tiles.GetTile(x, y).GetType() == typeof(StartTile))
                    Draw.Rect(new Vec2(x * Tile.Size.X * 2, y * Tile.Size.Y * 2) - Tile.Size * 0.9
                        , new Vec2(x * Tile.Size.X * 2, y * Tile.Size.Y * 2) + Tile.Size * 0.9, new Color(0, 0.2, 0));
            }
        level.Render();
    }

    public override void Render()
    {
        cam.Apply();
        Draw.Clear(Color.Black);
        //RenderTiles();
        Vec2 v = level.Tiles.GetSize();
        if(App.Width / App.Height < v.X / v.Y)
            Draw.Texture(world, Tile.Size, Tile.Size + new Vec2(v.X, v.X / (1.0 * App.Width / App.Height)));
        else Draw.Texture(world, Tile.Size, Tile.Size + new Vec2(v.Y * App.Width / App.Height, v.Y));
        if (vecForSaw1 != -1)
            Draw.Rect(Tiles.GetPosition(Tiles.GetCoords(vecForSaw1)) + Tile.Size,
                Tiles.GetPosition(Tiles.GetCoords(vecForSaw1)) - Tile.Size, Color.Green);
        RenderTileMenu();
        base.Render();
    }

    void RenderTileMenu()
    {
        Vec2 v = new Vec2(-Tile.Size.X * 1.1, -Tile.Size.Y * 1.1);
        RenderState.Push();
        new View(360).Apply();
		RenderState.Translate(new Vec2(240, 180));
        RenderState.Scale(0.5);

        if (currentTile == null)
            Draw.Rect(v + Tile.Size * 1.1, v - Tile.Size * 1.1, Color.Orange);
        for (int i = 0; i < TileTypes.Count; i++)
        {
            v += new Vec2(0, -Tile.Size.Y * 1.1 * 2);
            if (currentTile != null && currentTile.GetType() == Type.GetType(TileTypes[i]))
                Draw.Rect(v + Tile.Size * 1.1, v - Tile.Size * 1.1, Color.Orange);
            ((Tile)Type.GetType(TileTypes[i]).GetConstructor(new Type[] { }).Invoke(new object[] { })).SetPosition(v).Render();
        }
		RenderState.Pop();
    }

    void RenderSawsVectors()
    {
        foreach (var a in level.Tiles.GetMovingTiles())
            if (a.GetType() == typeof(Saw))
            {
                RenderState.Color = Color.Blue;
                Draw.Line(a.Position, Tiles.GetPosition(a.Link), 3);
            }
        
    }

    Texture world;
    void RefreshTexture()
    {
        if (world != null)
            world.Dispose();
        Vec2i v = level.Tiles.GetSize();
        int mult = Math.Min(Math.Max(1, Math.Max(v.X / App.Width, v.Y / App.Height)), 5);
        world = new Texture(App.Width * mult, App.Height * mult);
        RenderState.BeginTexture(world);
        View t = new View(Math.Max((double)v.Y, (double)v.X / (1.0 * App.Width / App.Height)));
        t.Position = new Vec2(t.FOV * App.Width / App.Height / 2, t.FOV / 2) + Tile.Size;
        t.Apply();
        RenderTiles();
        RenderSawsVectors();
        RenderState.EndTexture();
    }
}