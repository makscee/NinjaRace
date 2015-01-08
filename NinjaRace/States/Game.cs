using VitPro;
using VitPro.Engine;
using System;

class Game : State
{
    World world1, world2;
    bool multiplayer;

    public Game()
    {
        world1 = Program.GetWorld1();
        world2 = Program.GetWorld2();
        multiplayer = Program.GetWorld2() != null;
    }

    Texture w1, w2;

    public override void Render()
    {
        if (multiplayer)
            MultiRender();
        else SingleRender();
    }
    public override void Update(double dt)
    {
        if (world1.player.States.current is Win && (world2 == null || world2.player.States.current is Win))
        {
            Program.Manager.PushState(new AfterGame());
            Close();
        }
        world1.Update(dt);
        if(multiplayer)
            world2.Update(dt);
    }
    public override void KeyDown(Key key)
    {
        world1.KeyDown(key);
        if(multiplayer)
            world2.KeyDown(key);
        if (key == Key.Escape)
            Close();
            
    }

    public override void KeyUp(Key key)
    {
        world1.KeyUp(key);
        if (multiplayer)
            world2.KeyUp(key);
    }

    void MultiRender()
    {
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
    void SingleRender()
    {
        Draw.Clear(Color.Black);
        world1.Render();
    }
}