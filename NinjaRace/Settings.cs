using VitPro;
using VitPro.Engine;
using System;

[Serializable]
class Settings
{
    public bool Fullscreen = false,
        VSync = false;
    public Key P1Left = Key.A,
        P1Right = Key.D,
        P1Down = Key.S,
        P1Jump = Key.F,
        P1Bonus = Key.G,
        P1Sword = Key.H,
        P2Left = Key.Left,
        P2Right = Key.Right,
        P2Down = Key.Down,
        P2Jump = Key.Number1,
        P2Bonus = Key.Number2,
        P2Sword = Key.Number3;

    public void Apply()
    {
        App.Fullscreen = Fullscreen;
        App.VSync = VSync;
        GUtil.Dump(this, "./Data/Settings");
    }

    public PlayerController GetPlayer1Controller()
    {
        return new PlayerController(P1Left, P1Right, P1Down, P1Jump, P1Bonus, P1Sword);
    }

    public PlayerController GetPlayer2Controller()
    {
        return new PlayerController(P2Left, P2Right, P2Down, P2Jump, P2Bonus, P2Sword);
    }
}