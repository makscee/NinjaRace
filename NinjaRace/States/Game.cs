using VitPro;
using VitPro.Engine;
using System;

class Game : State
{
    World world = new World();

    public override void Render()
    {
        new Camera(240).Apply();
        Draw.Clear(Color.Black);
        world.Render();
    }
    public override void Update(double dt)
    {
        world.Update(dt);
    }
    public override void KeyDown(Key key)
    {
        world.KeyDown(key);
    }

    public override void KeyUp(Key key)
    {
        world.KeyUp(key);
    }
}