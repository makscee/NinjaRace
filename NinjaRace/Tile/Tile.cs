using VitPro;
using VitPro.Engine;
using System;

class Tile :IRenderable
{
    public static Vec2 Size = new Vec2(15, 15);
    private Vec2 _Position;

    public Vec2 Position 
    {
        get { return _Position; }
        set { _Position = value; Box = new CollisionBox(Position, Size); }
    }

    public CollisionBox Box;

    public Tile() { }

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

    public virtual void Render()
    {
        Draw.Rect(Position + Size, Position - Size, Color.White);
    }

    public virtual void Effect(Player player)
    {
    }
}