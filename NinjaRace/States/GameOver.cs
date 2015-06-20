using VitPro;
using VitPro.Engine;
using System;

class GameOver : Menu
{
    public GameOver(string player)
    {
        Button done = new Button("DONE", () => Program.Manager.NextState = new MainMenu(), 50);
        done.Anchor = new Vec2(0.5, 0.2);
        Frame.Add(done);

        Label congrats = new Label("CONGRATULATIONS " + player + "!", 65);
        congrats.Anchor = new Vec2(0.5, 0.7);
        Frame.Add(congrats);
    }
}