using VitPro;
using VitPro.Engine;
using VitPro.Engine.UI;
using System;

class TournamentGameOver : Menu
{
    View cam = new View(80);
    PlayerState Winner;
    public TournamentGameOver(Player player)
    {
        Button done = new Button("DONE", () => Program.Manager.NextState = new MainMenu(), 50, 200);
        done.Anchor = new Vec2(0.6, 0.2);
        AddExpandElement(done);

        Label Gameover = new Label("GAME OVER", 45);
        Gameover.Anchor = new Vec2(0.6, 0.9);
        Frame.Add(Gameover);

        player.Dir = 1;
        Winner = new Walking(player);
        cam.Position = player.Position + new Vec2(40, 0);
    }

    public override void RenderBackground()
    {
        RenderState.Push();
        cam.Apply();
        Winner.Render();
        RenderState.Pop();
        base.RenderBackground();
    }
    public override void Update(double dt)
    {
        base.Update(dt);
        Winner.Update(dt / 2);
    }
}