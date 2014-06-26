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
        player.Position = new Vec2(0, 50);
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
    }
}