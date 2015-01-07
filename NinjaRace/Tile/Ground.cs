using System;
using VitPro;
using VitPro.Engine;

[Serializable]
class Ground : Tile
{
    protected override void LoadTexture()
    {
        tex = new Texture("./Data/img/tiles/ground.png");
    }

    public override object Clone()
    {
        return new Ground().SetPosition(Position);
    }
}