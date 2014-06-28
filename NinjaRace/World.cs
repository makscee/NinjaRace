using VitPro;
using VitPro.Engine;
using System;

class World : IRenderable, IUpdateable
{

    Player player = new Player(new ControllerPlayer1());
    Tiles tiles = new Tiles();
    Camera cam = new Camera(240);

    public World()
    {
        player.Position = new Vec2(100, 100);
        for (int i = 0; i < 20; i++)
            tiles.Add(1, i);
        for (int i = 1; i < 5; i++)
            tiles.Add(i, 1);
        tiles.Add(2, 5);
    }

    public void Render()
    {
        cam.Apply();
        tiles.Render();
        player.Render();
    }

    public void Update(double dt)
    {
        player.Update(dt);
        tiles.PlayerCollision(player);
        tiles.PlayerFlyCheck(player);
        cam.Position += (player.Position - cam.Position) * dt * 10;
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