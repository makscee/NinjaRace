using VitPro;
using VitPro.Engine;
using System;
using System.Collections.Generic;

class VersusMenu : Menu
{
    List<string> Levels = new List<string>();
    int LevelNum = 0;
    Label Current;

    public VersusMenu()
    {
        Levels.AddRange(DBUtils.GetLevelNames());

        Current = new Label(Levels[LevelNum], 50);
        Current.Anchor = new Vec2(0.5, 0.5);

        Button Left = new Button("<", () => { LevelNum = (LevelNum - 1 + Levels.Count) % Levels.Count; Current.Text = Levels[LevelNum]; }, 50, 50);
        Left.Anchor = new Vec2(0.2, 0.5);

        Button Right = new Button(">", () => { LevelNum = (LevelNum + 1) % Levels.Count; Current.Text = Levels[LevelNum]; }, 50, 50);
        Right.Anchor = new Vec2(0.8, 0.5);

        Button Start = new Button("START", () =>
        {
            new Game(Levels[LevelNum]);
        }, 60, 200);
        Start.Anchor = new Vec2(0.5, 0.2);

        Frame.Add(Start);
        Frame.Add(Current);
        Frame.Add(Left);
        Frame.Add(Right);
    }
}