using VitPro;
using VitPro.Engine;
using System;

class ControllerPlayer2 : IController
{
    bool _NeedJump = false;
    public bool NeedJump()
    {
        bool t = _NeedJump;
        _NeedJump = false;
        return t;
    }

    public void KeyDown(Key key)
    {
        if (key == Key.ControlRight)
            _NeedJump = true;
    }

    public void KeyUp(Key key)
    {
        if (key == Key.ControlRight)
            _NeedJump = false;
    }

    public Vec2 NeedVel()
    {
        Vec2 t = Vec2.Zero;
        t += Key.Left.Pressed() ? new Vec2(-1, 0) : Vec2.Zero;
        t += Key.Right.Pressed() ? new Vec2(1, 0) : Vec2.Zero;
        return t;
    }
}