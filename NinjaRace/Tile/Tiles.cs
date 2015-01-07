﻿using VitPro;
using VitPro.Engine;
using System;
using System.Collections.Generic;

[Serializable]
class Tiles : IRenderable
{
    private Tile[,] tiles;

    public Tiles(int sizex, int sizey)
    {
        tiles = new Tile[sizex, sizey];
    }

    public StartTile GetStartTile()
    {
        foreach (var a in tiles)
            if (a is StartTile)
                return (StartTile)a;
        return null;
    }

    public void AddTile(int i, int j, Tile tile)
    {
        if (i <= 0 || j <= 0)
            return;
        if (i >= tiles.GetLength(0) || j >= tiles.GetLength(1))
            return;
        if (tile == null)
        {
            tiles[i, j] = null;
            return;
        }
        if (tile is StartTile && GetStartTile() != null)
            return;
        tile = (Tile)tile.Clone();
        tile.Position = new Vec2(Tile.Size.X * j * 2, Tile.Size.Y * i * 2);
        tiles[i, j] = tile;
    }

    public Tile GetTile(int i, int j)
    {
        if (i >= tiles.GetLength(0) || j >= tiles.GetLength(1) || i < 0 || j < 0)
            return null;
        return tiles[i, j];
    }

    public int GetLength(int d)
    {
        return tiles.GetLength(d);
    }

    public void Render()
    {
        foreach (var a in tiles)
            if(a != null)
                a.Render();
    }
}