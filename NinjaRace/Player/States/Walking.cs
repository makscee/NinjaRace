using VitPro;
using VitPro.Engine;
using System;

class Walking : PlayerState
{
    public Walking(Player player) : base(player) { }
    public override void Render()
    {
        Draw.Rect(player.Position + player.Size, player.Position - player.Size, Color.White);
    }
}