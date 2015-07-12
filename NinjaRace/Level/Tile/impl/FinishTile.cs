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
        if (Program.Manager.CurrentState is Game)
        {
            ((Game)Program.Manager.CurrentState).Finish(player);
        }
    }
}