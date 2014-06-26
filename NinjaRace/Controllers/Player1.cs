using VitPro;
using VitPro.Engine;
using System;

class ControllerPlayer1 : IController
{

    public bool NeedJump()
    {
        return Key.Space.Pressed();
    }

    public Vec2 NeedVel()
    {
        Vec2 t = Vec2.Zero;
        t += Key.A.Pressed() ? new Vec2(-1, 0) : Vec2.Zero;
        t += Key.D.Pressed() ? new Vec2(1, 0) : Vec2.Zero;
        return t;
    }
}