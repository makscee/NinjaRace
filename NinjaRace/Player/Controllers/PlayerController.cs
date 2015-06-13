using VitPro;
using VitPro.Engine;
using System;

abstract class PlayerController : IController
{
    bool _NeedJump = false, _NeedSwing = false, _NeedBonus = false;
    Vec2 _NeedDash = Vec2.Zero;
    protected Key sword, jump, bonus, moveUp, moveDown, moveLeft, moveRight;
    Key last;
    long ticks;
    double dashDelay = 0.3;
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
        last = key;
        if (key == jump)
            _NeedJump = true;
        if (key == sword)
            _NeedSwing = true;
        if (key == bonus)
            _NeedBonus = true;

        if (key == moveDown)
            _NeedDash = -Vec2.OrtY;
        if (key == moveLeft || key == moveRight)
        {
            if (last == key && DateTime.Now.Ticks - ticks < dashDelay * TimeSpan.TicksPerSecond)
            {
                ticks = 0;
                if (key == moveLeft)
                    _NeedDash = -Vec2.OrtX;
                if (key == moveDown)
                    _NeedDash = -Vec2.OrtY;
                if (key == moveRight)
                    _NeedDash = Vec2.OrtX;
            }
            else
            {
                last = key;
                ticks = DateTime.Now.Ticks;
            }
        }
    }

    public void KeyUp(Key key)
    {
        if (key == jump)
            _NeedJump = false;
        if (key == sword)
            _NeedSwing = false;
        if (key == bonus)
            _NeedBonus = false;
    }

    public Vec2 NeedVel()
    {
        Vec2 t = Vec2.Zero;
        t += moveLeft.Pressed() ? new Vec2(-1, 0) : Vec2.Zero;
        t += moveRight.Pressed() ? new Vec2(1, 0) : Vec2.Zero;
        t += moveDown.Pressed() ? new Vec2(0, -1) : Vec2.Zero;
        t += moveUp.Pressed() ? new Vec2(0, 1) : Vec2.Zero;
        return t;
    }
}