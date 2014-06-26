using VitPro;
using VitPro.Engine;
using System;

class Tile :IRenderable
{
    public static Vec2 Size = new Vec2(15, 15);
    public Vec2 Position;

    public CollisionBox Box;

    public Tile(Vec2 Position)
    {
        this.Position = Position;
        Box = new CollisionBox(Position, Size);
    }

    public Tile(double x, double y)
    {
        Position = new Vec2(x, y);
        Box = new CollisionBox(Position, Size);
    }

    public void Render()
    {
        Draw.Rect(Position + Size, Position - Size, Color.White);
    }
}