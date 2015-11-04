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
        RenderState.Push();
        RenderState.View2d(0, 0, Frame.Size.X, Frame.Size.Y);
        EffectsBot.Render();
        RenderState.Pop();
        base.Render();
        RenderState.Push();
        RenderState.View2d(0, 0, Frame.Size.X, Frame.Size.Y);
        EffectsTop.Render();
        RenderState.Pop();
    }

    private Group<Tuple<UI.Element, double>> ExpandEffect = new Group<Tuple<UI.Element, double>>();

    protected void AddExpandElement(UI.Element element)
    {
        EffectsTop.Add(new ExpandEffect(element));
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
            a.Item1.FixedWidth = a.Item1.FixedWidth + dt * 700;
            if (a.Item1.FixedWidth + 70 > a.Item2)
            {
                foreach (var e in EffectsTop)
                    if (e is ExpandEffect && ((ExpandEffect)e).Element == a.Item1)
                        ((ExpandEffect)e).Alive = false;
                if (a.Item1.FixedWidth > a.Item2)
                {
                    a.Item1.FixedWidth = a.Item2;
                    ExpandEffect.Remove(a);
                    Color c = a.Item1.TextColor;
                    c.A = 1;
                    a.Item1.TextColor = c;
                }
            }
        }
        ExpandEffect.Refresh();
        EffectsBot.Update(dt);
        EffectsTop.Update(dt);
        EffectsTop.Refresh();
        EffectsBot.Refresh();
    }

    public virtual void RenderBackground()
    {
    }

    UI.Element Selected = null;
    public override void KeyDown(Key key)
    {
        base.KeyDown(key);
        if (key == Key.Escape)
            Program.Manager.NextState = new MainMenu();
        if (key == Key.Down)
        {
            SelectNext(-Vec2.OrtY);
        }
        if (key == Key.Up)
        {
            SelectNext(Vec2.OrtY);
        }
        if (key == Key.Left)
        {
            SelectNext(-Vec2.OrtX);
        }
        if (key == Key.Right)
        {
            SelectNext(Vec2.OrtX);
        }
        if (key == Key.Enter && Selected != null)
        {
            if (Focus != null)
            {
                Focus.LoseFocus();
                Focus = null;
                SelectNext(-Vec2.OrtY);
                return;
            }
            if (Selected.Focusable)
            {
                Focus = Selected;
                Focus.Focus();
            }
            else Selected.Click();
        }
    }

    List<UI.Element> Elements = new List<UI.Element>();
    UI.Element Next;
    void SelectNext(Vec2 dir)
    {
        if (Elements.Count == 0)
        {
            foreach (var a in Frame.Children)
                if (a is UI.ElementList)
                {
                    foreach (var b in a.Children)
                        if(!(b is Label))
                            Elements.Add(b);
                }
                else if(!(a is Label)) Elements.Add(a);
        }
        if (Selected == null)
        {
            Selected = Elements[0];
            if (Selected.Focusable)
                Selected.Focused = true;
            else Selected.Hovered = true;
            return;
        }
        if (Focus != null)
            return;
        Vec2 SelectedVec = Selected.Position;
        if (dir.X == 1)
        {
            if (IsRight(SelectedVec))
                SelectedVec = new Vec2(0, SelectedVec.Y);
            List<UI.Element> t = Elements.FindAll(new Predicate<UI.Element>(Elem => Elem.Position.X > SelectedVec.X));
            t.Sort(new Comparison<UI.Element>(
                delegate(UI.Element e1, UI.Element e2) { return e1.Position.X.CompareTo(e2.Position.X); }));
            t = t.FindAll(new Predicate<UI.Element>(
                Elem => Elem.Position.X == t[0].Position.X));
            
            int result = t.BinarySearch(Selected, Comparer<UI.Element>.Create(
                delegate(UI.Element e1, UI.Element e2) { return e2.Position.Y.CompareTo(e1.Position.Y); }));
            if (result < 0)
                result = ~result == t.Count ? ~result - 1 : ~result;
            Next = t[result];
        }
        if (dir.X == -1)
        {
            if (IsLeft(SelectedVec))
                SelectedVec = new Vec2(App.Width, SelectedVec.Y);
            List<UI.Element> t = Elements.FindAll(new Predicate<UI.Element>(Elem => Elem.Position.X < SelectedVec.X));
            t.Sort(new Comparison<UI.Element>(
                delegate(UI.Element e1, UI.Element e2) { return e2.Position.X.CompareTo(e1.Position.X); }));
            t = t.FindAll(new Predicate<UI.Element>(
                Elem => Elem.Position.X == t[0].Position.X));

            int result = t.BinarySearch(Selected, Comparer<UI.Element>.Create(
                delegate(UI.Element e1, UI.Element e2) { return e2.Position.Y.CompareTo(e1.Position.Y); }));
            if (result < 0)
                result = ~result == t.Count ? ~result - 1 : ~result;
            Next = t[result];
        }
        if (dir.Y == 1)
        {
            if (IsTop(SelectedVec))
                SelectedVec = new Vec2(SelectedVec.X, 0);
            List<UI.Element> t = Elements.FindAll(new Predicate<UI.Element>(Elem => Elem.Position.Y > SelectedVec.Y));
            t.Sort(new Comparison<UI.Element>(
                delegate(UI.Element e1, UI.Element e2) { return e1.Position.Y.CompareTo(e2.Position.Y); }));
            t = t.FindAll(new Predicate<UI.Element>(
                Elem => Elem.Position.Y == t[0].Position.Y));
            int result = t.BinarySearch(Selected, Comparer<UI.Element>.Create(
                delegate(UI.Element e1, UI.Element e2) { return e1.Position.X.CompareTo(e2.Position.X); }));
            if (result < 0)
                result = ~result == t.Count ? ~result - 1 : ~result;
            Next = t[result];
        }
        if (dir.Y == -1)
        {
            if (IsBot(SelectedVec))
                SelectedVec = new Vec2(SelectedVec.X, App.Height);
            List<UI.Element> t = Elements.FindAll(new Predicate<UI.Element>(Elem => Elem.Position.Y < SelectedVec.Y));
            t.Sort(new Comparison<UI.Element>(
                delegate(UI.Element e1, UI.Element e2) { return e2.Position.Y.CompareTo(e1.Position.Y); }));
            t = t.FindAll(new Predicate<UI.Element>(
                Elem => Elem.Position.Y == t[0].Position.Y));
            int result = t.BinarySearch(Selected, Comparer<UI.Element>.Create(
                delegate(UI.Element e1, UI.Element e2) { return e1.Position.X.CompareTo(e2.Position.X); }));
            if (result < 0)
                result = ~result == t.Count ? ~result - 1 : ~result;
            Next = t[result];
        }
        if (Selected.Focusable)
            Selected.Focused = false;
        else Selected.Hovered = false;
        if (Next.Focusable)
            Next.Focused = true;
        else Next.Hovered = true;
        Selected = Next;
    }
    bool IsBot(Vec2 p)
    {
        foreach (var a in Elements)
        {
            if (a.Position.Y < p.Y)
                return false;
        }
        return true;
    }
    bool IsTop(Vec2 p)
    {
        foreach (var a in Elements)
        {
            if (a.Position.Y > p.Y)
                return false;
        }
        return true;
    }
    bool IsLeft(Vec2 p)
    {
        foreach (var a in Elements)
        {
            if (a.Position.X < p.X)
                return false;
        }
        return true;
    }
    bool IsRight(Vec2 p)
    {
        foreach (var a in Elements)
        {
            if (a.Position.X > p.X)
                return false;
        }
        return true;
    }
}