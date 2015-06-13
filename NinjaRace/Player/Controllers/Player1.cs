using VitPro;
using VitPro.Engine;
using System;

class ControllerPlayer1 : PlayerController
{
    public ControllerPlayer1()
    {
        sword = Key.G;
        jump = Key.F;
        bonus = Key.H;
        moveUp = Key.W;
        moveDown = Key.S;
        moveLeft = Key.A;
        moveRight = Key.D;
    }
}