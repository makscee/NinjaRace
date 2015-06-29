using System;
using VitPro;
using VitPro.Engine;

[Serializable]
class FinishTile : Tile
{
    protected override void LoadTexture()
    {
        tex = new AnimatedTexture(new Texture("./Data/img/tiles/FinishTile.png"));
    }

    public override void Effect(Player player, Side side)
    {
        if(!(player.States.current.GetType() == typeof(Win)))
            player.States.Set(new Win(player));
    }
}