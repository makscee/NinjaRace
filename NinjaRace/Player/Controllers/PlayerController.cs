using VitPro;
using VitPro.Engine;
using System;

class PlayerController : IController
{
    bool _NeedJump = false, _NeedSwing = false, _NeedBonus = false;
    Vec2 _NeedDash = Vec2.Zero;
    protected Key Sword, Jump, Bonus, MoveDown, MoveLeft, MoveRight;
    Key Last;
    long Ticks;
    double DashDelay = 0.3;
    public PlayerController(Key moveLeft, Key moveRight, Key moveDown,
        Key jump, Key bonus, Key sword)
    {
        MoveLeft = moveLeft;
        MoveRight = moveRight;
        MoveDown = moveDown;
        Jump = jump;
        Bonus = bonus;
        Sword = sword;
    }
    public bool NeedJump()
    {
        bool t = _NeedJump;
        _NeedJump = false;
        return t;
    }

    public Vec2 NeedDash()
    {
        Vec2 t = _NeedDash;
        _NeedDash = Vec2.Zero;
        return t;
    }

    public bool NeedBonus()
    {
        bool t = _NeedBonus;
        _NeedBonus = false;
        return t;
    }

    public bool NeedSwing()
    {
        bool t = _NeedSwing;
        _NeedSwing = false;
        return t;
    }

    public void KeyDown(Key key)
    {
        if (key == Jump)
            _NeedJump = true;
        if (key == Sword)
            _NeedSwing = true;
        if (key == Bonus)
            _NeedBonus = true;

        if (key == MoveDown)
            _NeedDash = -Vec2.OrtY;
        if (key == MoveLeft || key == MoveRight)
        {
            if (Last == key && DateTime.Now.Ticks - Ticks < DashDelay * TimeSpan.TicksPerSecond)
            {
                Ticks = 0;
                if (key == MoveLeft)
                    _NeedDash = -Vec2.OrtX;
                if (key == MoveDown)
                    _NeedDash = -Vec2.OrtY;
                if (key == MoveRight)
                    _NeedDash = Vec2.OrtX;
            }
            else
            {
                Last = key;
                Ticks = DateTime.Now.Ticks;
            }
        }
    }

    public void KeyUp(Key key)
    {
        if (key == Jump)
            _NeedJump = false;
        if (key == Sword)
            _NeedSwing = false;
        if (key == Bonus)
            _NeedBonus = false;
    }

    public Vec2 NeedVel()
    {
        Vec2 t = Vec2.Zero;
        t += MoveLeft.Pressed() ? new Vec2(-1, 0) : Vec2.Zero;
        t += MoveRight.Pressed() ? new Vec2(1, 0) : Vec2.Zero;
        t += MoveDown.Pressed() ? new Vec2(0, -1) : Vec2.Zero;
        return t;
    }
}