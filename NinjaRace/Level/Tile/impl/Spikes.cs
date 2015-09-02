using VitPro;
using VitPro.Engine;
using System;

[Serializable]
class Spikes : Tile
{
    
    protected override void LoadTexture()
    {
        tex = new AnimatedTexture(new Texture("./Data/img/tiles/spikes.png"));
    }

    public override void Effect(Player player, Side side)
    {
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
    }
}