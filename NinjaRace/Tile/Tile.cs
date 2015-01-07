using VitPro;
using VitPro.Engine;
using System;

[Serializable]
abstract class Tile : IRenderable, ICloneable
{
    [field:NonSerialized]
    protected Texture tex;
    public static Vec2 Size = new Vec2(15, 15);
    private Vec2 _Position;
    protected bool Mark = false;

    public Vec2 Position 
    {
        get { return _Position; }
        set { _Position = value; Box = Mark? null : new CollisionBox(Position, Size); }
    }

    public Tile SetPosition(Vec2 pos)
    {
        Position = pos;
        return this;
    }

    public CollisionBox Box;

    protected virtual void LoadTexture() { }

    public virtual void Render()
    {
        if(tex == null)
            LoadTexture();
        Draw.Save();
        Draw.Translate(Position);
        Draw.Scale(Tile.Size.X, Tile.Size.Y);
        Draw.Scale(2);
        Draw.Align(0.5, 0.5);
        tex.Render();
        Draw.Load();
    }

    public virtual void Effect(Player player, Side side) { }

    public abstract object Clone();
}