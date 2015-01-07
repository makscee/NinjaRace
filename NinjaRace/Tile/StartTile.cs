using System;
using VitPro;
using VitPro.Engine;

[Serializable]
class StartTile : Tile
{

    public StartTile()
    {
        Mark = true;
    }

    public override void Render()
    {
    }

    public override object Clone()
    {
        return new StartTile().SetPosition(Position);
    }
}
