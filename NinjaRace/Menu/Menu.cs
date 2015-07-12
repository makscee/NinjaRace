using VitPro;
using VitPro.Engine;
using UI = VitPro.Engine.UI;
using System;

class Menu : UI.State
{
    public Group<Effect> EffectsTop = new Group<Effect>();
    public Group<Effect> EffectsBot = new Group<Effect>();

    public override void Render()
    {
        Draw.Clear(Color.Black);
        RenderBackground();
        base.Render();
    }

    public virtual void RenderBackground()
    {
    }

    public override void KeyDown(Key key)
    {
        base.KeyDown(key);
        if (key == Key.Escape)
            Close();
    }
}