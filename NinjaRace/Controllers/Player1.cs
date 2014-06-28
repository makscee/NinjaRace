using VitPro;
using VitPro.Engine;
using System;

class ControllerPlayer1 : IController
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
        if (key == Key.Space)
            _NeedJump = true;
    }

    public void KeyUp(Key key)
    {
        if (key == Key.Space)
            _NeedJump = false;
    }

    public Vec2 NeedVel()
    {
        Vec2 t = Vec2.Zero;
        t += Key.A.Pressed() ? new Vec2(-1, 0) : Vec2.Zero;
        t += Key.D.Pressed() ? new Vec2(1, 0) : Vec2.Zero;
        return t;
    }
}