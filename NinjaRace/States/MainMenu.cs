using VitPro;
using VitPro.Engine;
using System;

class MainMenu : Menu
{
    public MainMenu()
    {
        buttons.Add(new Button(new Vec2(0, 50), new Vec2(80, 40)).SetText("PLAY")
            .SetAction(() => Program.Manager.PushState(new Game())));
        buttons.Add(new Button(new Vec2(0, -50), new Vec2(80, 20)).SetText("LEVEL EDITOR")
            .SetAction(() => Program.Manager.PushState(new LevelEditor())));
        buttons.Refresh();
    }
}