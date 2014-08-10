﻿using VitPro;
using VitPro.Engine;
using System;
using System.Collections.Generic;

class Tiles : IRenderable
{
    private Tile[,] tiles = new Tile[10, 50];

    public Tiles()
    {
    }

    public void AddTile(int i, int j)
    {
        if (i <= 0 || j <= 0)
            return;
        if (i >= tiles.GetLength(0) || j >= tiles.GetLength(1))
            return;
        tiles[i, j] = new Tile(Tile.Size.X * j * 2, Tile.Size.Y * i * 2);
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