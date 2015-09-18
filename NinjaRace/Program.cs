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

    static void Main()
    {
        try
        {
            Settings = GUtil.Load<Settings>("./Data/Settings");
        }
        catch (FileNotFoundException)
        {
            Settings = new Settings();
        }
        Settings.Apply();
        font.Smooth = false;
        Manager = new MyManager(new MainMenu());
        new Showdown("FIRST_S", true);
        DBUtils.Init();
        App.Run(Manager);
    }
}