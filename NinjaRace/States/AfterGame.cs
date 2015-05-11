using System;
using VitPro;
using VitPro.Engine;

class AfterGame : Menu
{
    public AfterGame()
    {
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