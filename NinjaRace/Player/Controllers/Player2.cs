using VitPro;
using VitPro.Engine;
using System;

class ControllerPlayer2 : IController
{

    bool _NeedJump = false, _NeedSwing = false, _NeedBonus = false;
    Key sword = Key.Keypad2, jump = Key.Keypad1, bonus = Key.Keypad3,
        moveUp = Key.Up,
        moveDown = Key.Down,
        moveLeft = Key.Left,
        moveRight = Key.Right;
    public bool NeedJump()
    {
        bool t = _NeedJump;
        _NeedJump = false;
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
        if (key == jump)
            _NeedJump = true;
        if (key == sword)
            _NeedSwing = true;
        if (key == bonus)
            _NeedBonus = true;
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