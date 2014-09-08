using VitPro;
using VitPro.Engine;
using System;

class ControllerPlayer1 : IController
{
    bool _NeedJump = false;
    bool _NeedAbility = false;
    public bool NeedJump()
    {
        bool t = _NeedJump;
        _NeedJump = false;
        return t;
    }

    public bool NeedAbility()
    {
        bool t = _NeedAbility;
        _NeedAbility = false;
        return t;
    }

    public void KeyDown(Key key)
    {
        if (key == Key.Space)
            _NeedJump = true;
        if (key == Key.ControlLeft)
            _NeedAbility = true;
    }

    public void KeyUp(Key key)
    {
        if (key == Key.Space)
            _NeedJump = false;
        if (key == Key.ControlLeft)
            _NeedAbility = false;
    }

    public Vec2 NeedVel()
    {
        Vec2 t = Vec2.Zero;
        t += Key.A.Pressed() ? new Vec2(-1, 0) : Vec2.Zero;
        t += Key.D.Pressed() ? new Vec2(1, 0) : Vec2.Zero;
        t += Key.S.Pressed() ? new Vec2(0, -1) : Vec2.Zero;
        t += Key.W.Pressed() ? new Vec2(0, 1) : Vec2.Zero;
        return t;
    }
}