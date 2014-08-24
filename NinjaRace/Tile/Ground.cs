using System;
using VitPro;
using VitPro.Engine;

[Serializable]
class Ground : Tile
{
    public override object Clone()
    {
        return new Ground().SetPosition(Position);
    }

    public override void Render()
    {
        Draw.Rect(Position + Size, Position - Size, Color.White);
    }
}