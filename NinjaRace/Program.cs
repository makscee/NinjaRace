using VitPro;
using VitPro.Engine;
using System;
using System.IO;

class Program
{
    public static Random Random = new Random();
    public static MyManager Manager;
	public static Font font = new Font("./Data/font.TTF", 30, Font.Style.Bold);
    public static bool VSync = false;

    public static World World;

    static void Main()
    {
        font.Smooth = false;
        Manager = new MyManager(new MainMenu());
        App.Fullscreen = false;
        App.VSync = VSync;
        App.Run(Manager);
    }

    public static Vec2 MousePosition()
    {
        Vec2 pos = Mouse.Position;
        pos += new Vec2(-320, -240);
        pos /= 2;
        pos = new Vec2(pos.X * 640 / App.Width, pos.Y * 480 / App.Height);
        return pos;
    }
}