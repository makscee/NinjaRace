using VitPro;
using VitPro.Engine;
using System;

class Menu : State
{
    protected Group<Button> buttons = new Group<Button>();
    protected Group<EnterField> efields = new Group<EnterField>();
    protected Group<DisplayField> dfields = new Group<DisplayField>();
    public Group<Effect> EffectsTop = new Group<Effect>();
    public Group<Effect> EffectsBot = new Group<Effect>();
    private Camera cam = new Camera(240);

    public void AddDField(DisplayField f)
    {
        dfields.Add(f);
        dfields.Refresh();
    }

    public void AddButton(Button b)
    {
        buttons.Add(b);
        buttons.Refresh();
    }

    public void AddEField(EnterField f)
    {
        efields.Add(f);
        efields.Refresh();
    }

    public override void Render()
    {
        cam.Apply();
        Draw.Clear(Color.Black);
        EffectsBot.Render();
        buttons.Render();
        efields.Render();
        dfields.Render();
        EffectsTop.Render();
    }

    public override void Update(double dt)
    {
        EffectsBot.Update(dt);
        EffectsTop.Update(dt);
    }

    public override void MouseDown(MouseButton button, Vec2 pos)
    {
        if (button == MouseButton.Left)
        {
            foreach (var a in buttons)
                a.Click();
            foreach (var a in efields)
                a.Click();
        }
    }

    public override void KeyDown(Key key)
    {
        foreach (var a in efields)
            a.Enter(key);
        if (key == Key.Escape)
            Close();
    }
}