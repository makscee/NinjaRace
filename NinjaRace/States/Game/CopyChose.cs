using VitPro;
using VitPro.Engine;
using System;

class CopyChose : State
{
    World World;
    bool Player1;
    Player Player;

    public CopyChose(Player player)
    {
        World = Program.World;
        Player = player;
        Player.DoRender = false;
        Player1 = World.Player1 == player;
    }

    double T = 0;
    public override void Render()
    {
        base.Render();
        World.RenderSingle();
        RenderState.Push();
        new View(1).Apply();
        Draw.Rect(new Vec2(-1, -1), new Vec2(1, 1), new Color(0, 0, 0, T > 0.3 ? 0.6 : T * 2));
        RenderState.Pop();
    }

    public override void KeyDown(Key key)
    {
        base.KeyDown(key);
        if (key == (Player1 ? Program.Settings.P1Left : Program.Settings.P2Left))
        {
            AddCopy(1);
            AddCopy(0);
            Player.Position += new Vec2(-30, 0);
            Close();
            Player.DoRender = true;
        }
        if (key == (Player1 ? Program.Settings.P1Right : Program.Settings.P2Right))
        {
            AddCopy(-1);
            AddCopy(0);
            Player.Position += new Vec2(30, 0);
            Close();
            Player.DoRender = true;
        }
        if (key == (Player1 ? Program.Settings.P1Down : Program.Settings.P2Down))
        {
            AddCopy(1);
            AddCopy(-1);
            Close();
            Player.DoRender = true;
        }
    }

    public override void Update(double dt)
    {
        base.Update(dt);
        T += dt;
        Player.Velocity = Vec2.Zero;
        if (T > 3)
        {
            AddCopy(1);
            AddCopy(-1);
            Close();
            Player.DoRender = true;
            return;
        }
        World.Update(dt / 10);
    }

    void AddCopy(int pos)
    {
        Player p = new Player(Player.Position + new Vec2(30 * pos, 0), Player.Color)
            .SetControls(World.Player1 == Player ?
                Program.Settings.GetPlayer1Controller() : Program.Settings.GetPlayer2Controller());
        p.Velocity = Player.Velocity;
        p.Dir = Player.Dir;
        World.Copies[Player].Add(p);
        Timer t = new Timer(8, () => 
        {
            p.States.current.Die(p.Position);
        });
        p.NextDeath += () => t.Drop();
        Player.NextDeath += () => t.Complete();
    }
}