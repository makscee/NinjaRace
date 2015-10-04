using VitPro;
using VitPro.Engine;
using UI = VitPro.Engine.UI;
using System;

class PlayMenu : Menu
{
    public PlayMenu()
    {
        Button start = new Button("VERSUS",
            () => { Program.Manager.PushState(new VersusMenu()); }, 50, 400);
        start.Anchor = new Vec2(0.5, 0.7);
        AddExpandElement(start);

        Button settings = new Button("TOURNAMENT",
            () => { Program.Manager.PushState(new TournamentMenu()); }, 50, 400);
        settings.Anchor = new Vec2(0.5, 0.3);
        AddExpandElement(settings);
    }
}