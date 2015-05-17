using VitPro;
using VitPro.Engine;
using System;

class ControllerPlayer2 : IController
{
    bool _NeedJump = false, _NeedSwing = false;
    public bool NeedJump()
    {
        bool t = _NeedJump;
        _NeedJump = false;
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
        if (key == Key.ControlRight)
            _NeedJump = true;
        if (key == Key.ShiftRight)
            _NeedSwing = true;
    }

    public void KeyUp(Key key)
    {
        if (key == Key.ControlRight)
            _NeedJump = false;
        if (key == Key.ShiftRight)
            _NeedSwing = false;
    }

    public Vec2 NeedVel()
    {
        Vec2 t = Vec2.Zero;
        t += Key.Left.Pressed() ? new Vec2(-1, 0) : Vec2.Zero;
        t += Key.Right.Pressed() ? new Vec2(1, 0) : Vec2.Zero;
        t += Key.Down.Pressed() ? new Vec2(0, -1) : Vec2.Zero;
        t += Key.Up.Pressed() ? new Vec2(0, 1) : Vec2.Zero;
        return t;
    }
}