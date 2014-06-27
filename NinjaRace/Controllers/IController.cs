using VitPro;
using System;
using VitPro.Engine;

interface IController
{
    bool NeedJump();
    Vec2 NeedVel();
}