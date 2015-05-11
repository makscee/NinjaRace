using VitPro;
using VitPro.Engine;
using System;
using System.Collections.Generic;

partial class Player
{
    public void CalculateCollisions()
    {
        Tiles tiles = Game.World.level.tiles;
        collisions.Clear();
        collisions.Add(Side.Left, new List<Tile>());
        collisions.Add(Side.Right, new List<Tile>());
        collisions.Add(Side.Up, new List<Tile>());
        collisions.Add(Side.Down, new List<Tile>());
        int xbound = (int)Math.Floor(Position.X / Tile.Size.X / 2), ybound = (int)Math.Floor(Position.Y / Tile.Size.Y / 2);
        for (int y = ybound - 1; y <= ybound + 1; y++)
            for (int x = xbound - 1; x <= xbound + 1; x++)
            {
                Tile t = tiles.GetTile(x, y);
                if (t != null && CollisionBox.Collide(Box, t.Box) && ((t.Position - Position).Length < (Tile.Size + Size).Length - 1))
                    collisions[Box.Collide(t.Box)].Add(t);
            }
        foreach(Tile t in tiles.GetCustomTiles())
            if(CollisionBox.Collide(Box, t.Box) && ((t.Position - Position).Length < (Tile.Size + Size).Length - 1))
                collisions[Box.Collide(t.Box)].Add(t);
    }

    public void CollisionHits()
    {
        foreach (Side s in collisions.Keys)
            foreach (Tile t in collisions[s])
            {
                switch (s)
                {
                    case Side.Right:
                        {
                            if(CollisionBox.CollideThrough(Box, t.Box))
                                Position = new Vec2(t.Position.X - Tile.Size.X - Size.X, Position.Y);
                            Velocity = (Velocity.X > 0)? new Vec2(0, Velocity.Y) : Velocity;
                            break;
                        }
                    case Side.Left:
                        {
                            if (CollisionBox.CollideThrough(Box, t.Box))
                                Position = new Vec2(t.Position.X + Tile.Size.X + Size.X, Position.Y);
                            Velocity = (Velocity.X < 0) ? new Vec2(0, Velocity.Y) : Velocity;
                            break;
                        }
                    case Side.Up:
                        {
                            if (CollisionBox.CollideThrough(Box, t.Box))
                                Position = new Vec2(Position.X, t.Position.Y - Tile.Size.Y - Size.Y);
                            Velocity = (Velocity.Y > 0) ? new Vec2(Velocity.X, 0) : Velocity;
                            break;
                        }
                    case Side.Down:
                        {
                            if (CollisionBox.CollideThrough(Box, t.Box))
                                Position = new Vec2(Position.X, t.Position.Y + Tile.Size.Y + Size.Y);
                            Velocity = (Velocity.Y < 0) ? new Vec2(Velocity.X, 0) : Velocity;
                            break;
                        }
                }
            }
    }

    public bool TouchWalls()
    {
        return collisions[Side.Left].Count > 0 ||
            collisions[Side.Right].Count > 0 ||
            collisions[Side.Down].Count > 0;
    }

    public bool OnGround()
    {
        return collisions[Side.Down].Count > 0;
    }
}