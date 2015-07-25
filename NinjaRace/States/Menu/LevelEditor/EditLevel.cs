using VitPro;
using VitPro.Engine;
using System;
using System.Collections.Generic;

class EditLevel : Menu
{
    List<string> Levels = new List<string>();
    int LevelNum = 0;
    Label Current;
    CheckBox Showdown = new CheckBox(20);

    public EditLevel()
    {
        Levels.AddRange(DBUtils.GetLevelNames());

        Current = new Label(Levels[LevelNum], 50);
        Current.Anchor = new Vec2(0.5, 0.5);

        Button Left = new Button("<", () => { LevelNum = (LevelNum - 1 + Levels.Count) % Levels.Count; Current.Text = Levels[LevelNum]; }, 50);
        Left.Anchor = new Vec2(0.2, 0.5);
        Button Right = new Button(">", () => { LevelNum = (LevelNum + 1) % Levels.Count; Current.Text = Levels[LevelNum]; }, 50);
        Right.Anchor = new Vec2(0.8, 0.5);

        Label LShowdown = new Label("SHOWDOWN:", 30);
        LShowdown.Anchor = new Vec2(0.5, 0.38);
        LShowdown.BackgroundColor = Color.TransparentBlack;

        Showdown.Anchor = new Vec2(0.67, 0.385);

        Button Done = new Button("DONE", () =>
        {
            Close();
            Program.Manager.PushState(new LevelEditor(Levels[LevelNum], Showdown.Checked));
        }, 60);
        Done.Anchor = new Vec2(0.5, 0.2);

        Frame.Add(Done);
        Frame.Add(Current);
        Frame.Add(Left);
        Frame.Add(Right);
        Frame.Add(LShowdown);
        Frame.Add(Showdown);
    }
}