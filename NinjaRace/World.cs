using VitPro;
using VitPro.Engine;
using System;

class World : IRenderable, IUpdateable
{

    public Player player1, player2;
    public Level level;
    Camera cam1 = new Camera(540), cam2 = new Camera(540);
    Vec2 camOffset = new Vec2(0, 120);
    public double Time = 0;
    public Texture background;

    public World()
    {
        level = DBUtils.GetLevel("default");
        background = new Texture("./Data/img/background.png");
        player1 = new Player().SetControls(new ControllerPlayer1());
        player1.Position = level.tiles.GetStartTile().Position;
        player2 = new Player().SetControls(new ControllerPlayer2());
        player2.Position = level.tiles.GetStartTile().Position;
    }

    public void Render()
    {
        Texture tex = new Texture(Draw.Width, Draw.Height);
        Draw.BeginTexture(tex);
        Camera camt = new Camera(cam1.FOV);
        camt.Position = cam1.Position + camOffset;
        camt.Apply();
        DrawBackground(player1);
        level.Render();
        player2.Render();
        player1.Render();
        Draw.EndTexture();

        Draw.Save();
        Draw.Scale(2);
        Draw.Align(0.5, 0.5);
        Draw.Translate(0, 0.5);
        tex.Render();
        Draw.Load();

        tex = new Texture(Draw.Width, Draw.Height);
        Draw.BeginTexture(tex);
        camt = new Camera(cam2.FOV);
        camt.Position = cam2.Position - camOffset;
        camt.Apply();
        DrawBackground(player2);
        level.Render();
        player1.Render();
        player2.Render();
        Draw.EndTexture();

        Draw.Save();
        Draw.Scale(2);
        Draw.Align(0.5, 0.5);
        Draw.Translate(0, -0.5);
        tex.Render();
        Draw.Load();
    }

    private void UpdateForPlayer(double dt, Player player)
    {
        if (!(player.States.current is Win))
            Time += dt;
        player.CalculateCollisions();
        player.Update(dt);
        Camera cam = (player == player1) ? cam1 : cam2;
        cam.Position += (player.Position - cam.Position) * dt * 10;
        player.Position += player.Velocity * dt;

        foreach (var a in player.collisions[Side.Left])
            a.Effect(player, Side.Left);
        foreach (var a in player.collisions[Side.Down])
            a.Effect(player, Side.Down);
        foreach (var a in player.collisions[Side.Right])
            a.Effect(player, Side.Right);
        foreach (var a in player.collisions[Side.Up])
            a.Effect(player, Side.Up);
        level.Update(dt);
    }

    public void Update(double dt)
    {
        UpdateForPlayer(dt, player1);
        UpdateForPlayer(dt, player2);
    }
    public void KeyDown(Key key)
    {
        player1.Controller.KeyDown(key);
        player2.Controller.KeyDown(key);
    }
    public void KeyUp(Key key)
    {
        player1.Controller.KeyUp(key);
        player2.Controller.KeyUp(key);
    }

    private void DrawBackground(Player player)
    {
        Draw.Save();
        new Camera(360).Apply();
        Vec2 offset = new Vec2(player.Position.X / 3, 0);
        offset = new Vec2(offset.X - App.Width * Math.Floor(offset.X / App.Width), 0);
        Draw.Translate(-offset);
        Draw.Scale(App.Width / 2, App.Height / 3);
        Draw.Scale(2);
        Draw.Align(0.5, 0.5);
        background.Render();
        Draw.Translate(new Vec2(1, 0));
        background.Render();
        Draw.Load();
    }
}