using VitPro;
using VitPro.Engine;
using UI = VitPro.Engine.UI;
using System;

class MainMenu : Menu
{
    public MainMenu()
    {
        Button start = new Button("START", () => { Program.Manager.PushState(new Game()); }, 50);
        start.Anchor = new Vec2(0.5, 0.8);
        Frame.Add(start);

        Button le = new Button("LEVEL EDITOR", () => { Program.Manager.PushState(new LevelEditorMenu()); }, 50);
        le.Anchor = new Vec2(0.5, 0.4);
        Frame.Add(le);
    }
}