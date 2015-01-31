using VitPro;
using VitPro.Engine;
using System;

[Serializable]
class Saw : Tile
{
    Vec2 pos1, pos2;
    bool forward = true;
    double speed = 100;

    protected override void LoadTexture()
    {
        tex = new Texture("./Data/img/tiles/saw.png");
    }

    public Saw() { }

    public Saw(Vec2 p1, Vec2 p2)
    {
        Position = p1;
        pos1 = p1;
        pos2 = p2;
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
        if (pos1.Equals(pos2))
            return;

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