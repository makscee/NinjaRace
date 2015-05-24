using VitPro;
using VitPro.Engine;
using System;

[Serializable]
class Saw : Tile
{
    bool forward = true;
    double speed = 100;

    protected override void LoadTexture()
    {
        tex = new AnimatedTexture()
            .Add(new Texture("./Data/img/tiles/saw.png"), 0.2)
            .Add(new Texture("./Data/img/tiles/saw2.png"), 0.2);
    }

    public Saw() 
    {
        Moving = true;
    }

    public Saw SetSpeed(double s)
    {
        speed = s;
        return this;
    }

    public override void Effect(Player player, Side side)
    {
        player.States.current.Die(Position);
    }

    public override void Update(double dt)
    {
        base.Update(dt);
        if (Link == -1)
            return;
        Vec2 pos1 = Tiles.GetPosition(Tiles.GetCoords(ID));
        Vec2 pos2 = Tiles.GetPosition(Tiles.GetCoords(Link));
        Vec2 v = forward ? (pos2 - pos1).Unit : (pos1 - pos2).Unit;
        Position += v * speed * dt;
        if (forward && Vec2.Dot(pos2 - Position, v) < 0)
        {
            forward = false;
            return;
        }
        if (!forward && Vec2.Dot(pos1 - Position, v) < 0)
        {
            forward = true;
            return;
        }


    }
}