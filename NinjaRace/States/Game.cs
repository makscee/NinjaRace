using VitPro;
using VitPro.Engine;
using System;

class Game : State
{
    World World;

    public Game(string level)
    {
        World = new World(level);
        Program.Manager.PushState(this);
        Program.Manager.PushState(new PreGame(World));
    }

    public override void Render()
    {
        Draw.Clear(Color.Black);
        World.RenderSplit();
    }
    public override void Update(double dt)
    {
        dt = Math.Min(dt, 1.0 / 60);
        World.Update(dt);
        TimerContainer.Update(dt);
    }
    public void Finish(Player player)
    {
        new Showdown(World.level.Name.Trim() + "_S", World.player1 == player ? true : false);
        Close();
    }
    public override void KeyDown(Key key)
    {
        World.KeyDown(key);
        if (key == Key.Escape)
            Close();
    }

    public override void KeyUp(Key key)
    {
        World.KeyUp(key);
    }
}