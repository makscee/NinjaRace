using VitPro;
using VitPro.Engine;
using System;

class Menu : State
{
    protected Group<Button> buttons = new Group<Button>();
    protected Group<EnterField> fields = new Group<EnterField>();
    protected Group<DisplayField> dfields = new Group<DisplayField>();
    private Camera cam = new Camera(240);

    public override void Render()
    {
        cam.Apply();
        Draw.Clear(Color.Black);
        buttons.Render();
        fields.Render();
        dfields.Render();
    }

    public override void MouseDown(MouseButton button, Vec2 pos)
    {
        if (button == MouseButton.Left)
        {
            foreach (var a in buttons)
                a.Click();
            foreach (var a in fields)
                a.Click();
        }
    }

    public override void KeyDown(Key key)
    {
        foreach (var a in fields)
            a.Enter(key);
        if (key == Key.Escape)
            Close();
    }
}