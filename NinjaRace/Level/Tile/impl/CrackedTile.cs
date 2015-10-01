using VitPro;
using VitPro.Engine;
using System;

class CrackedTile : Tile
{
    public CrackedTile()
    {
        Colorable = true;
    }
    protected override void LoadTexture()
    {
        tex = new AnimatedTexture(new Texture("./Data/img/tiles/cracked.png"));
    }
    public override void Effect(Player player, Side side)
    {
        new Timer(0.5, () => { Program.World.Level.Tiles.DeleteTile(ID); });
    }
}