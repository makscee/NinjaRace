using VitPro;
using VitPro.Engine;
using System;

class BonusOnScreen : Effect
{
    Vec2 size = new Vec2(3, 3);

    public Player player;

    public override void Render()
    {
        if (tex == null)
        {
            RenderState.Push();
            RenderState.Color = Color.Red;
            Draw.Rect(Position + size, Position - size);
            RenderState.Pop();
        }
        else Draw.Texture(tex, Position - size, Position + size);
    }

    Texture tex;

    public BonusOnScreen(Texture tex, Player player)
        : base(Program.World.player1 == player ? new Vec2(-70, 40) : new Vec2(70, -40))
    {
        this.tex = tex;
        this.player = player;
    }

    public override void Dispose()
    {
        base.Dispose();
        if(tex != null)
            tex.Dispose();
    }
}