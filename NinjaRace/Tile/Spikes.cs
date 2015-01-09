using VitPro;
using VitPro.Engine;
using System;

[Serializable]
class Spikes : Tile
{
    protected override void LoadTexture()
    {
        tex = new Texture("./Data/img/tiles/spikes.png");
    }

    public override void Effect(Player player, Side side)
    {
        player.States.current.Die(Position);
    }
}