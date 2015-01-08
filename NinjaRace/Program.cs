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
    static World world1, world2;

    static void Main()
    {
        font.Smooth = false;
        Manager = new MyManager(new MainMenu());
        App.Fullscreen = false;
        App.VSync = VSync;
        App.Run(Manager);
    }

    public static void InitWorld1(Player player)
    {
        world1 = new World(0, player);
    }

    public static void InitWorld2(Player player)
    {
        world1 = new World(1, world1.player);
        world2 = new World(2, player);
    }

    public static World GetWorld1()
    {
        return world1;
    }

    public static World GetWorld2()
    {
        return world2;
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