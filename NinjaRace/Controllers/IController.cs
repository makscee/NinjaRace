using VitPro;
using System;
using VitPro.Engine;

interface IController
{
    bool NeedJump();
    Vec2 NeedVel();
    void KeyDown(Key key);
    void KeyUp(Key key);
}