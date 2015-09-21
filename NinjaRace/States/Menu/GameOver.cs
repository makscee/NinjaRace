using VitPro;
using VitPro.Engine;
using System;

class GameOver : Menu
{
    View cam = new View(80);
    Player Winner;
    public GameOver(Player player)
    {
        Button done = new Button("DONE", () => Program.Manager.NextState = new MainMenu(), 50, 200);
        done.Anchor = new Vec2(0.6, 0.2);
        AddExpandElement(done);

        Label Gameover = new Label("GAME OVER", 45);
        Gameover.Anchor = new Vec2(0.6, 0.7);
        Frame.Add(Gameover);
        Winner = player;
        Winner.Controller = new PlayerController();
        Winner.Position = Vec2.Zero;
    }

    public override void RenderBackground()
    {
        RenderState.Push();
        cam.Position = Winner.Position + new Vec2(35, 0);
        cam.Apply();
        Winner.Render();
        RenderState.Pop();
        base.RenderBackground();
    }
    public override void Update(double dt)
    {
        base.Update(dt);
        Winner.Update(dt / 2);
        //cam.FOV = Math.Sin((double)System.DateTime.Now.Ticks / 1e8) / 2 + 1.6;
    }
}