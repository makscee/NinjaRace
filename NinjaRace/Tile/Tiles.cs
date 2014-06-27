using VitPro;
using VitPro.Engine;
using System;
using System.Collections.Generic;

class Tiles : IRenderable
{
    Tile[,] tiles = new Tile[10, 50];

    public Tiles()
    {
    }

    public void Add(int i, int j)
    {
        if (i <= 0 || j <= 0)
            return;
        if (i >= tiles.GetLength(0) || j >= tiles.GetLength(1))
            return;
        tiles[i, j] = new Tile(Tile.Size.X * j * 2, Tile.Size.Y * i * 2);
    }

    public void Render()
    {
        foreach (var a in tiles)
            if(a != null)
                a.Render();
    }

    public void PlayerCollision(Player p)
    {
        int xbound = (int)Math.Floor(p.Position.X / Tile.Size.X / 2), ybound = (int)Math.Floor(p.Position.Y / Tile.Size.Y / 2);
        for(int i = ybound - 1; i <= ybound + 1 && i < tiles.GetLength(0); i++)
            for (int j = xbound - 1; j <= xbound + 1 && j < tiles.GetLength(1); j++)
            {
                if (i < 0 || j < 0)
                    continue;
                if (tiles[i, j] != null && CollisionBox.Collide(p.Box, tiles[i, j].Box))
                    p.State.CollideWith(tiles[i, j]);
            }
    }
    public void PlayerFlyCheck(Player p)
    {
        Vec2 p1 = p.Position - p.Size - new Vec2(0, 1), p2 = p.Position - new Vec2(-p.Size.X, p.Size.Y + 1);
        int j1 = (int)Math.Round(p1.X / Tile.Size.X / 2), j2 = (int)Math.Round(p2.X / Tile.Size.X / 2), i1 = (int)Math.Round(p1.Y / Tile.Size.Y / 2), i2 = (int)Math.Round(p2.Y / Tile.Size.Y / 2);
        if (i1 >= 0 && j1 >= 0 && i1 < tiles.GetLength(0) && j1 <= tiles.GetLength(1) && tiles[i1, j1] != null
            || i2 >= 0 && j2 >= 0 && i2 < tiles.GetLength(0) && j2 <= tiles.GetLength(1) && tiles[i2, j2] != null)
        {
            if (p.State is Flying || p.State is WallGrab)
                p.State = new Walking(p);
            return;
        }
        if (p.State is Walking)
            p.State = new Flying(p);

    }
}