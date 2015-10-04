using VitPro;
using VitPro.Engine;
using System;
using System.Collections.Generic;

class TournamentMenu : Menu
{
    List<string> Levels = new List<string>();
    int LevelNum = 0;
    Label Current;

    public TournamentMenu()
    {
        Levels.AddRange(DBUtils.GetLevelNames());

        Current = new Label(Levels[LevelNum], 50);
        Current.Anchor = new Vec2(0.5, 0.4);

        TextInput Players = new TextInput(60);
        Players.Anchor = new Vec2(0.5, 0.6);
        Label PlayersLabel = new Label("PLAYERS AMOUNT", 30);
        PlayersLabel.Anchor = new Vec2(0.5, 0.7);

        Button Left = new Button("<", () => { LevelNum = (LevelNum - 1 + Levels.Count) % Levels.Count; Current.Text = Levels[LevelNum]; }, 50, 50);
        Left.Anchor = new Vec2(0.2, 0.4);

        Button Right = new Button(">", () => { LevelNum = (LevelNum + 1) % Levels.Count; Current.Text = Levels[LevelNum]; }, 50, 50);
        Right.Anchor = new Vec2(0.8, 0.4);

        Button Start = new Button("START", () =>
        {
            Program.Tournament = new Tournament(int.Parse(Players.Value), Levels[LevelNum]);
            Program.Manager.NextState = Program.Tournament;
        }, 60, 200);
        Start.Anchor = new Vec2(0.5, 0.15);

        Frame.Add(Start);
        Frame.Add(Current);
        Frame.Add(Left);
        Frame.Add(Right);
        Frame.Add(Players);
        Frame.Add(PlayersLabel);
    }
}