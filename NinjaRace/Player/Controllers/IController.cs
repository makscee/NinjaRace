using VitPro;
using System;
using VitPro.Engine;

interface IController
{
    bool NeedJump();
    bool NeedSwing();
    Vec2 NeedVel();
    bool NeedBonus();
    void KeyDown(Key key);
    void KeyUp(Key key);
}