using VitPro;
using VitPro.Engine;
using System;

class World : IRenderable, IUpdateable
{

    Player player;
    Tiles tiles = new Tiles(10, 50);
    public Camera cam = new Camera(360);
    public Vec2 camOffset = Vec2.Zero;

    public World(int mode)
    {
        tiles = GUtil.Load<Tiles>("./level.dat");
        switch (mode)
        {
            case 1:
                {
                    player = new Player(new ControllerPlayer1());
                    camOffset = new Vec2(0, 120);
                    player.Position = new Vec2(100, 100);
                    cam.FOV *= 1.5;
                    break;
                }
            case 2:
                {
                    player = new Player(new ControllerPlayer2());
                    camOffset = new Vec2(0, -120);
                    player.Position = new Vec2(100, 100);
                    cam.FOV *= 1.5;
                    break;
                }
            default:
                {
                    player = new Player(new ControllerPlayer1());
                    player.Position = new Vec2(100, 100);
                    break;
                }
        }
    }

    public void Render()
    {
        Camera camt = new Camera(cam.FOV);
        camt.Position = cam.Position + camOffset;
        camt.Apply();
        tiles.Render();
        player.Render();
    }

    public void Update(double dt)
    {
        player.CalculateCollisions(tiles);
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
    }
    public void KeyDown(Key key)
    {
        player.Controller.KeyDown(key);
    }
    public void KeyUp(Key key)
    {
        player.Controller.KeyUp(key);
    }
}