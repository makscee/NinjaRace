using VitPro;
using VitPro.Engine;
using System;

class KeyPress : Menu
{
    Action<Key> KeySet;
    Label Text;
    public KeyPress(Action<Key> a)
    {
        KeySet = a;
        Text = new Label("PRESS A KEY", 80);
        Text.BackgroundColor = Color.TransparentBlack;
        Text.Anchor = new Vec2(0.5, 0.5);
        AddElement(Text);
    }

    public override void KeyDown(Key key)
    {
        KeySet.Apply(key);
        Close();
    }

    public override void Render()
    {
        base.Render();
    }
}