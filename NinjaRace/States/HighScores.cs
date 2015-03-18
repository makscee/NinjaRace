using System;
using VitPro;
using VitPro.Engine;

class HighScores : Menu
{
    public HighScores()
    {
        dfields.Add(new DisplayField(new Vec2(0, 35), new Vec2(90, 80))
            .SetName("Game Over")
            .SetColors(new Color(0.2, 0.2, 0.2), Color.White)
            .SetTextOffset(0, 0.7)
        );
        dfields.Add(new DisplayField(new Vec2(0, 55), new Vec2(80, 10))
            .SetColors(new Color(0.1, 0.1, 0.1), Color.White)
            .SetTextFromTheLeft()
            .SetTextScale(12)
            .SetName("Player 1 Time: " + Math.Round(Program.GetWorld1().Time, 2).ToString()));
        buttons.Add(new Button(new Vec2(0, -100), new Vec2(40, 15))
            .SetName("Done")
            .SetAction(() =>
            {
                Close();
                Program.Manager.PushState(new MainMenu());
            }
        ));

        int i = 0;
        foreach(var a in DBUtils.GetHighScores())
        {
            dfields.Add(new DisplayField(new Vec2(0, 55 - i), new Vec2(80, 10))
                .SetColors(new Color(0.1, 0.1, 0.1), Color.White)
                .SetTextFromTheLeft()
                .SetTextScale(12)
                .SetName(a.Item1 + ": " + a.Item2));
            i += 25;
        }

        dfields.Refresh();
        buttons.Refresh();
    }

}