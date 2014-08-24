using VitPro;
using VitPro.Engine;
using System;

[Serializable]
abstract class Tile : IRenderable, ICloneable
{
    public static Vec2 Size = new Vec2(15, 15);
    private Vec2 _Position;

    public Vec2 Position 
    {
        get { return _Position; }
        set { _Position = value; Box = new CollisionBox(Position, Size); }
    }

    public Tile SetPosition(Vec2 pos)
    {
        Position = pos;
        return this;
    }

    public CollisionBox Box;

    public Tile() { }

    public abstract void Render();

    public virtual void Effect(Player player, Side side) { }

    public abstract object Clone();
}