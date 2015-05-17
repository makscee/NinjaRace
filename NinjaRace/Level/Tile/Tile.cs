using VitPro;
using VitPro.Engine;
using System;

[Serializable]
abstract class Tile : IRenderable, IUpdateable
{
    [field:NonSerialized]
    protected AnimatedTexture tex;

    public int ID;
    public static Vec2 Size = new Vec2(15, 15);
    private Vec2 _Position;
    protected bool Mark = false;
    protected bool Moving = false;
    public int Link = -1;

    public Vec2 Position 
    {
        get { return _Position; }
        set { _Position = value; Box = new CollisionBox(Position, Size); }
    }

    public bool IsMoving { get { return Moving; } }
    public bool IsMark { get { return Mark; } }

    public int GetY()
    {
        double t = Position.Y;
        return (int)Math.Round(t / Tile.Size.Y / 2);
    }

    public int GetX()
    {
        double t = Position.X;
        return (int)Math.Round(t / Tile.Size.X / 2);
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
        tex.RenderToPosAndSize(Position, Tile.Size);
    }

    public virtual void Effect(Player player, Side side) { }

    public virtual void Update(double dt) 
    {
        if(tex != null)
            tex.Update(dt);
    }
}