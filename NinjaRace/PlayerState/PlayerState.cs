using VitPro;
using VitPro.Engine;
using System;

class PlayerState : IRenderable, IUpdateable
{
    protected Player Player;
    protected double Speed = 150, Acc = 900;
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
        Player.Velocity -= Vec2.Clamp(Player.Velocity - Player.Controller.NeedVel() * Speed, Acc * dt);
        if (Player.Controller.NeedJump())
        {
            Player.State = new Flying(Player);
            Player.Velocity = new Vec2(Player.Velocity.X, 200);
        }
    }
    public virtual void CollideWith(Tile t)
    {
        Side s = Player.Box.Collide(t.Box);
        switch (s)
        {
            case Side.Right:
                {
                    Player.Position = new Vec2(t.Position.X - Tile.Size.X - Player.Size.X, Player.Position.Y);
                    Player.Velocity = new Vec2(0, Player.Velocity.Y);
                    break;
                }
            case Side.Left:
                {
                    Player.Position = new Vec2(t.Position.X + Tile.Size.X + Player.Size.X, Player.Position.Y);
                    Player.Velocity = new Vec2(0, Player.Velocity.Y);
                    break;
                }
            case Side.Up:
                {
                    Player.Position = new Vec2(Player.Position.X, t.Position.Y - Tile.Size.Y - Player.Size.Y);
                    Player.Velocity = new Vec2(Player.Velocity.X, 0);
                    break;
                }
            case Side.Down:
                {
                    Player.Position = new Vec2(Player.Position.X, t.Position.Y + Tile.Size.Y + Player.Size.Y);
                    Player.Velocity = new Vec2(Player.Velocity.X, 0);
                    break;
                }
        }
    }
}