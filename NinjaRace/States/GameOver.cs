using VitPro;
using VitPro.Engine;
using System;

class GameOver : Menu
{
    Texture end;
    Camera cam = new Camera(1);
    public GameOver(string player, Texture endScreen)
    {
        end = endScreen;

        Button done = new Button("DONE", () => Program.Manager.NextState = new MainMenu(), 50);
        done.Anchor = new Vec2(0.5, 0.2);
        Frame.Add(done);

        Label congrats = new Label("CONGRATULATIONS " + player + "!", 45);
        congrats.Anchor = new Vec2(0.5, 0.7);
        Frame.Add(congrats);
    }

    public override void RenderBackground()
    {
        RenderState.Push();
        cam.Apply();
        RenderState.Color = new Color(1, 1, 1, 0.5);
        Draw.Texture(end, -new Vec2(1, 1), new Vec2(1, 1));
        RenderState.Pop();
        base.RenderBackground();
    }
    public override void Update(double dt)
    {
        base.Update(dt);
        cam.FOV = Math.Sin((double)System.DateTime.Now.Ticks / 1e8) + 1.1;
    }
}