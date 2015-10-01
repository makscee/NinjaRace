using VitPro;
using VitPro.Engine;
using System;
using System.IO;

class Program
{
    public static Random Random = new Random();
    public static MyManager Manager;
    public static Font font = new Font(new System.IO.FileStream("./Data/font.TTF", FileMode.Open, FileAccess.Read), 30, Font.Style.Bold);

    public static World World;
    public static Settings Settings;
    public static Statistics Statistics;

    static void Main()
    {
        Statistics = new Statistics();
        try
        {
            Settings = GUtil.Load<Settings>("./Data/Settings");
        }
        catch (Exception)
        {
            Settings = new Settings();
        }
        Settings.Apply();
        font.Smooth = false;
        World = new World("FIRST");
        Manager = new MyManager(new MainMenu());
        DBUtils.Init();
        App.Run(Manager);
    }

    public static bool IsCopy(Player player)
    {
        return World.Copies[World.Player1].Contains(player) || World.Copies[World.Player2].Contains(player);
    }

    public static int WhichPlayer(Player player)
    {
        return IsCopy(player) ? -1 : (player == World.Player1 ? 0 : 1);
    }
}