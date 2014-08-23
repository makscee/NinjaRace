using VitPro;
using VitPro.Engine;
using System;
using System.IO;

class Program
{
    public static Random Random = new Random();
    public static MyManager Manager;
    public static Font font = new Font("./Data/font.TTF", 30, FontStyle.Bold);
    public static bool VSync = false;

    static void Main()
    {
        Manager = new MyManager(new MainMenu());
        App.Fullscreen = false;
        App.VSync = VSync;
        font.Smooth = false;
        App.Run(Manager);
    }
}