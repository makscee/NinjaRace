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
    public bool Colorable = false;
    public int Link = -1;
    public Color Color;
    protected Shader Shader;

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

    double t = 0;
    protected double rotation = 0;

    public virtual void Render()
    {
        if (Shader == null)
            return;
        RenderState.Push();
        RenderState.Translate(Position);
        RenderState.Scale(Tile.Size * 2);
        RenderState.Rotate(rotation);
        RenderState.Origin(0.5, 0.5);
        RenderState.Set("color", Color);
        RenderState.Set("size", Math.Sin(t) + 1);
        SetAdditionalParameters();
        Shader.RenderQuad();
        RenderState.Pop();
    }

    public virtual void Effect(Player player, Side side) { }

    public virtual void Update(double dt) 
    {
        t += dt;
    }

    protected virtual void SetAdditionalParameters() { }
}