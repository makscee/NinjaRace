using VitPro;
using VitPro.Engine;
using VitPro.Engine.UI;
using System;

class GameOver : Menu
{
    View cam = new View(80);
    PlayerState Winner;
    public GameOver(Player player)
    {
        Button done = new Button("DONE", () => Program.Manager.NextState = new MainMenu(), 50, 200);
        done.Anchor = new Vec2(0.6, 0.2);
        AddExpandElement(done);

        Label Gameover = new Label("GAME OVER", 45);
        Gameover.Anchor = new Vec2(0.5, 0.9);
        Frame.Add(Gameover);

        ElementList names = new ElementList();
        names.Add(new Label("DEATHS", 23));
        names.Add(new Label("KILLS", 23));
        names.Add(new Label("BONUSES", 23));
        names.Horizontal = false;
        names.Anchor = new Vec2(0.4, 0.5);
        names.Visit((Element e) => e.FixedWidth = 130);
        names.Spacing = 20;
        Frame.Add(names);

        Label p1 = new Label("PLAYER 1", 23);
        Label p2 = new Label("PLAYER 2", 23);
        p1.FixedWidth = 110;
        p2.FixedWidth = 110;
        p1.Anchor = new Vec2(0.65, 0.75);
        p2.Anchor = new Vec2(0.85, 0.75);
        Frame.Add(p1);
        Frame.Add(p2);

        ElementList p1stats = new ElementList();
        p1stats.Add(new Label(Program.Statistics.Deaths[0].ToString(), 23));
        p1stats.Add(new Label(Program.Statistics.Kills[0].ToString(), 23));
        p1stats.Add(new Label(Program.Statistics.Bonuses[0].ToString(), 23));
        p1stats.Horizontal = false;
        p1stats.Anchor = new Vec2(0.65, 0.5);
        p1stats.Visit((Element e) => e.FixedWidth = 110);
        p1stats.Spacing = 20;
        Frame.Add(p1stats);

        ElementList p2stats = new ElementList();
        p2stats.Add(new Label(Program.Statistics.Deaths[1].ToString(), 23));
        p2stats.Add(new Label(Program.Statistics.Kills[1].ToString(), 23));
        p2stats.Add(new Label(Program.Statistics.Bonuses[1].ToString(), 23));
        p2stats.Horizontal = false;
        p2stats.Anchor = new Vec2(0.85, 0.5);
        p2stats.Visit((Element e) => e.FixedWidth = 110);
        p2stats.Spacing = 20;
        Frame.Add(p2stats);

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