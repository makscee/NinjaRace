using VitPro;
using VitPro.Engine;
using System;

[Serializable]
class Spikes : Tile
{
    public Spikes()
    {
        Shader = new Shader(NinjaRace.Shaders.Spikes); 
        Color = new Color(0.3, 0.3, 0.3);
    }

    public override void Effect(Player player, Side side)
    {
        if (player.States.IsDead)
            return;
        if (!player.collisions[Side.Down].Contains(this))
            player.States.current.Die(Position);
        else
        {
            foreach (var a in player.collisions[Side.Down])
            {
                if (a.GetType() == typeof(Ground))
                    return;
            }
            player.States.current.Die(Position);
        }
        colorBlend = 0.7;
    }
    double colorBlend = 0;
    public override void Update(double dt)
    {
        base.Update(dt);
        colorBlend = colorBlend < 0 ? 0 : colorBlend - dt;
        Vec2i pos = Tiles.GetCoords(ID);
        Tiles tiles = Program.World.Level.Tiles;
        if (pos.Y == 1 || tiles.GetTile(pos.X, pos.Y - 1) != null)
        {
            rotation = 0;
            return;
        }
        if (pos.Y == tiles.GetLength(0) || tiles.GetTile(pos.X, pos.Y + 1) != null)
        {
            rotation = Math.PI;
            return;
        }
        if (pos.X == 1 || tiles.GetTile(pos.X - 1, pos.Y) != null)
        {
            rotation = Math.PI * 3 / 2;
            return;
        }
        if (pos.X == tiles.GetLength(1) || tiles.GetTile(pos.X + 1, pos.Y) != null)
        {
            rotation = Math.PI / 2;
            return;
        }
    }

    protected override void SetAdditionalParameters()
    {
        RenderState.Set("color", new Color(Color.R + colorBlend, Color.G, Color.B, Color.A));
    }
}