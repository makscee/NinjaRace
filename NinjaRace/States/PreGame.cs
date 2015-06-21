using VitPro;
using VitPro.Engine;
using System;
using System.Collections.Generic;

class PreGame : Menu
{
    List<string> levels = new List<string>();
    int levelNum = 0;
    Label current;

    public PreGame()
    {
        levels.AddRange(DBUtils.GetLevelNames());

        current = new Label(levels[levelNum], 50);
        current.Anchor = new Vec2(0.5, 0.5);

        Button left = new Button("<", () => { levelNum = (levelNum - 1 + levels.Count) % levels.Count; current.Text = levels[levelNum]; }, 50);
        left.Anchor = new Vec2(0.2, 0.5);

        Button right = new Button(">", () => { levelNum = (levelNum + 1) % levels.Count; current.Text = levels[levelNum]; }, 50);
        right.Anchor = new Vec2(0.8, 0.5);

        Button start = new Button("START", () => { Program.Manager.NextState = new Game(levels[levelNum]); }, 60);
        start.Anchor = new Vec2(0.5, 0.2);

        Frame.Add(start);
        Frame.Add(current);
        Frame.Add(left);
        Frame.Add(right);
    }
}