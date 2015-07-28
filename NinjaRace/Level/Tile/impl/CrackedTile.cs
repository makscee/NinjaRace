using VitPro;
using VitPro.Engine;
using System;

class CrackedTile : Tile
{
    public override void Effect(Player player, Side side)
    {
        new Timer(0.5, () => { Program.World.level.Tiles.DeleteTile(ID); });
    }

    public override void Render()
    {
        RenderState.Push();
        RenderState.Color = Color.Yellow;
        Draw.Rect(Position - Size, Position + Size);
        RenderState.Pop();
    }
}