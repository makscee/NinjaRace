using VitPro;
using VitPro.Engine;
using System;

class ControllerPlayer2 : PlayerController
{
    public ControllerPlayer2()
    {
        sword = Key.Keypad2;
        jump = Key.Keypad1;
        bonus = Key.Keypad3;
        moveUp = Key.Up;
        moveDown = Key.Down;
        moveLeft = Key.Left;
        moveRight = Key.Right;
    }
}