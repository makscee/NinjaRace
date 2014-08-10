using VitPro;
using VitPro.Engine;
using System;

class Game : State
{
    World world1, world2;

    public Game()
    {
        world1 = new World(1);
        world2 = new World(2);
    }

    Texture w1, w2;

    public override void Render()
    {
        //new Camera(240).Apply();
        if (w1 == null || w1.Width != Draw.Width || w1.Height != Draw.Height)
            w1 = new Texture(Draw.Width, Draw.Height);
        if (w2 == null || w2.Width != Draw.Width || w2.Height != Draw.Height)
            w2 = new Texture(Draw.Width, Draw.Height);
        Draw.BeginTexture(w1);
        Draw.Clear(Color.Black);
        world1.Render();
        Draw.EndTexture();
        Draw.BeginTexture(w2);
        Draw.Clear(Color.Black);
        world2.Render();
        Draw.EndTexture();

        Draw.Save();
        Draw.Scale(2);
        Draw.Align(0.5, 0.5);
        Draw.Translate(0, 0.5);
        w1.Render();
        Draw.Load();

        Draw.Save();
        Draw.Scale(2);
        Draw.Align(0.5, 0.5);
        Draw.Translate(0, -0.5);
        w2.Render();
        Draw.Load();
    }
    public override void Update(double dt)
    {
        world1.Update(dt);
        world2.Update(dt);
    }
    public override void KeyDown(Key key)
    {
        world1.KeyDown(key);
        world2.KeyDown(key);
    }

    public override void KeyUp(Key key)
    {
        world2.KeyUp(key);
        world1.KeyUp(key);
    }
}