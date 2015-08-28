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
        Zoom = 1.0 * App.Height / 480;
        Draw.Clear(Color.Black);
        RenderBackground();
        base.Render();
    }

    private Group<Tuple<UI.Element, double>> ExpandEffect = new Group<Tuple<UI.Element, double>>();

    protected void AddExpandElement(UI.Element element)
    {
        ExpandEffect.Add(new Tuple<UI.Element, double>(element, (double)element.FixedWidth));
        ExpandEffect.Refresh();
        element.FixedWidth = 50;
        element.TextColor = Color.MultAlpha(element.TextColor, 0);
        AddElement(element);
    }

    protected void AddElement(UI.Element element)
    {
        Frame.Add(element);
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