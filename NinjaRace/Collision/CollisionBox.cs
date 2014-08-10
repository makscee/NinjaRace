using VitPro;
using System;

enum Side
{
    Left, Right, Down, Up, None
}

class CollisionBox
{
    double Left, Right, Down, Up;
    public CollisionBox(Vec2 Position, Vec2 Size)
    {
        Left = Position.X - Size.X;
        Right = Position.X + Size.X;
        Down = Position.Y - Size.Y;
        Up = Position.Y + Size.Y;
    }
    public static bool Collide(CollisionBox a, CollisionBox b)
    {
        if (b.Right + 1 < a.Left || b.Left - 1 > a.Right || b.Down - 1 > a.Up || b.Up + 1 < a.Down)
            return false;
        return true;
    }
    public static bool CollideThrough(CollisionBox a, CollisionBox b)
    {
        if (b.Right < a.Left || b.Left > a.Right || b.Down > a.Up || b.Up < a.Down)
            return false;
        return true;
    }
    public Side Collide(CollisionBox a)
    {
        if (!Collide(this, a))
            return Side.None;
        double 
            L = Math.Abs(Left - a.Right),
            R = Math.Abs(Right - a.Left),
            D = Math.Abs(Down - a.Up),
            U = Math.Abs(Up - a.Down);
        if (D <= L && D <= R && D <= U)
            return Side.Down;
        if (U <= L && U <= R && U <= D)
            return Side.Up;
        if (L <= R && L <= D && L <= U)
            return Side.Left;
        if (R <= L && R <= D && R <= U)
            return Side.Right;
        return Side.None;
    }
    
}