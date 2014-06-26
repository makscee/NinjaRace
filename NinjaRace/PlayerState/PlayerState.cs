using VitPro;
using VitPro.Engine;
using System;

class PlayerState : IRenderable, IUpdateable
{
    Player Player;
    public PlayerState(Player Player)
    {
        this.Player = Player;
    }
    public virtual void Render()
    {
        Draw.Rect(Player.Position + Player.Size, Player.Position - Player.Size, Color.White);
    }

    public virtual void Update(double dt)
    {
        
    }
    public virtual void CollideWith(Tile t)
    {
        Side s = Player.Box.Collide(t.Box);
        switch (s)
        {
            case Side.Right:
                {
                    Player.Position = new Vec2(t.Position.X - Tile.Size.X - Player.Size.X, Player.Position.Y);
                    break;
                }
            case Side.Left:
                {
                    Player.Position = new Vec2(t.Position.X + Tile.Size.X + Player.Size.X, Player.Position.Y);
                    break;
                }
            case Side.Up:
                {
                    Player.Position = new Vec2(Player.Position.X, t.Position.Y - Tile.Size.Y - Player.Size.Y);
                    break;
                }
            case Side.Down:
                {
                    Player.Position = new Vec2(Player.Position.X, t.Position.Y + Tile.Size.Y + Player.Size.Y);
                    break;
                }
        }
    }
}