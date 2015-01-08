using System;
using VitPro;
using VitPro.Engine;

class AfterGame : Menu
{
    public AfterGame()
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
        if(Program.GetWorld2() != null)
            dfields.Add(new DisplayField(new Vec2(0, 30), new Vec2(80, 10))
                .SetColors(new Color(0.1, 0.1, 0.1), Color.White)
                .SetTextFromTheLeft()
                .SetTextScale(12)
                .SetName("Player 2 Time: " + Math.Round(Program.GetWorld2().Time, 2).ToString()));
        buttons.Add(new Button(new Vec2(0, -100), new Vec2(40, 15))
            .SetName("Done")
            .SetAction(() =>
            {
                Close();
                Program.Manager.PushState(new MainMenu());
            }
        ));
        dfields.Refresh();
        buttons.Refresh();
    }

}