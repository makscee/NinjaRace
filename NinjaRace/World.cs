using VitPro;
using VitPro.Engine;
using System;

class World : IRenderable, IUpdateable
{

    public Player player;
    public Tiles Tiles = new Tiles(10, 50);
    Camera cam = new Camera(360);
    Vec2 camOffset = Vec2.Zero;
    public double Time = 0;
    public Texture background;

    public World(int mode, Player player)
    {
        background = new Texture("./Data/img/background.png");
        this.player = player;
        Tiles = GUtil.Load<Tiles>("./level.dat");
        switch (mode)
        {
            case 1:
                {
                    camOffset = new Vec2(0, 120);
                    player.Position = Tiles.GetStartTile().Position;
                    cam.FOV *= 1.5;
                    break;
                }
            case 2:
                {
                    camOffset = new Vec2(0, -120);
                    player.Position = Tiles.GetStartTile().Position;
                    cam.FOV *= 1.5;
                    break;
                }
            default:
                {
                    player.Position = Tiles.GetStartTile().Position;
                    break;
                }
        }
    }

    public void Render()
    {
        Camera camt = new Camera(cam.FOV);
        camt.Position = cam.Position + camOffset;
        camt.Apply();
        DrawBackground();
        Tiles.Render();
        player.Render();
    }

    public void Update(double dt)
    {
        
        if(!(player.States.current is Win))
            Time += dt;
        player.CalculateCollisions();
        player.Update(dt);
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
        Tiles.Update(dt);
    }
    public void KeyDown(Key key)
    {
        player.Controller.KeyDown(key);
    }
    public void KeyUp(Key key)
    {
        player.Controller.KeyUp(key);
    }

    private void DrawBackground()
    {
        Draw.Save();
        Draw.Translate(cam.Position - new Vec2(player.Position.X / 4, 0));
        Draw.Scale(App.Width / 2, App.Height / 3);
        Draw.Scale(2);
        Draw.Align(0.5, 0.5);
        Draw.Translate(new Vec2(Math.Round(player.Position.X / App.Width / 5), 0));
        background.Render();
        Draw.Translate(new Vec2(1, 0));
        background.Render();
        Draw.Load();
    }
}