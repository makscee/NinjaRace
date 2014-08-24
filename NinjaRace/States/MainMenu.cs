using VitPro;
using VitPro.Engine;
using System;

class MainMenu : Menu
{
    public MainMenu()
    {
        buttons.Add(new Button(new Vec2(0, 80), new Vec2(80, 20))
            .SetName("SINGLE PLAYER")
            .SetAction(() => Program.Manager.PushState(new Game(false))));
        buttons.Add(new Button(new Vec2(0, 30), new Vec2(80, 20))
            .SetName("MULTI PLAYER")
            .SetAction(() => Program.Manager.PushState(new Game(true))));
        buttons.Add(new Button(new Vec2(0, -50), new Vec2(80, 20))
            .SetName("LEVEL EDITOR")
            .SetAction(() => Program.Manager.PushState(new LevelEditorMenu())));
        buttons.Refresh();
    }
}