using VitPro;
using VitPro.Engine;
using System;

class DropTile : Tile
{
    public DropTile()
    {
        Moving = true;
    }

    double speed = 100;
    bool dropping = false;
    Player player;

    public override void Effect(Player player, Side side)
    {
        if (side == Side.Down)
        {
            dropping = true;
            this.player = player;
        }
    }

    public override void Update(double dt)
    {
        if (!dropping)
            return;
        foreach (var a in player.collisions[Side.Down])
            if (!a.IsMark && !(a is DropTile))
            {
                dropping = false;
                return;
            }
        Vec2 v = new Vec2(0, dt * speed);
        Position -= v;
        player.Position = new Vec2(player.Position.X, Position.Y + Size.Y + player.Size.Y);
        dropping = player.collisions[Side.Down].Contains(this);
    }

    public override void Render()
    {
        Draw.Rect(Position - Size, Position + Size, Color.Gray);
    }
}