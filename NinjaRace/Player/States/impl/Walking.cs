using VitPro;
using VitPro.Engine;
using System;

class Walking : PlayerState
{
    AnimatedTexture tex;
    public Walking(Player player) : base(player) { }
    public override void Render()
    {
        Draw.Texture(GetTexture().GetCurrent(), player.Position - player.Size, player.Position + player.Size);
    }
    public override AnimatedTexture GetTexture()
    {
        if (tex == null)
        {
            tex = new AnimatedTexture();
            for (int i = 1; i < 20; i++)
            {
                tex.Add(new Texture("./Data/img/player/player" + i.ToString() + ".png"), 0.05);
            }
        }
        return tex;
    }

}