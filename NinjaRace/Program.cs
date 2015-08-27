﻿using VitPro;
using VitPro.Engine;
using System;
using System.IO;

class Program
{
    public static Random Random = new Random();
    public static MyManager Manager;
    public static Font font = new Font(new System.IO.FileStream("./Data/font.TTF", FileMode.Open, FileAccess.Read), 30, Font.Style.Bold);
    public static bool VSync = false;

    public static World World;

    static void Main()
    {
        font.Smooth = false;
        Manager = new MyManager(new MainMenu());
        //new Game("TEST");
        App.Fullscreen = false;
        App.VSync = VSync;
        DBUtils.Init();
        App.Run(Manager);
    }
}