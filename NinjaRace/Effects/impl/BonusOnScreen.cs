using VitPro;
using VitPro.Engine;
using System;

class BonusOnScreen : Effect
{
    Vec2 size = new Vec2(3, 3);

    public override void Render()
    {
        if (tex == null)
            Draw.Rect(Position + size, Position - size, Color.Red);
        else tex.Render();
    }

    Texture tex;

    public BonusOnScreen(Texture tex)
        : base(new Vec2(-70, 40))
    {
        this.tex = tex;
    }

    public override void Dispose()
    {
        base.Dispose();
        if(tex != null)
            tex.Dispose();
    }
}