using VitPro;
using VitPro.Engine;
using UI = VitPro.Engine.UI;
using System;
using System.Collections.Generic;

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

    private Group<Tuple<UI.Element, int>> ExpandEffect = new Group<Tuple<UI.Element, int>>();

    protected void AddExpandElement(UI.Element element, int width)
    {
        ExpandEffect.Add(new Tuple<UI.Element, int>(element, width));
        ExpandEffect.Refresh();
        element.FixedWidth = 50;
        element.TextColor = Color.MultAlpha(element.TextColor, 0);
    }

    public override void Update(double dt)
    {
        base.Update(dt);
        foreach (var a in ExpandEffect)
        {
            a.Item1.FixedWidth = a.Item1.FixedWidth + dt * 1000;
            if (a.Item1.FixedWidth > a.Item2)
            {
                a.Item1.FixedWidth = a.Item2;
                ExpandEffect.Remove(a);
                Color c = a.Item1.TextColor;
                c.A = 1;
                a.Item1.TextColor = c;
            }
        }
        ExpandEffect.Refresh();
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