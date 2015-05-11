using VitPro;
using VitPro.Engine;
using System;

class Menu : State
{
    protected Group<Button> buttons = new Group<Button>();
    protected Group<EnterField> efields = new Group<EnterField>();
    protected Group<DisplayField> dfields = new Group<DisplayField>();
    private Camera cam = new Camera(240);

    public override void Render()
    {
        cam.Apply();
        Draw.Clear(Color.Black);
        buttons.Render();
        efields.Render();
        dfields.Render();
        //new Camera(3).Apply();
        //Draw.Align(0.5, 0.5);
        //Shader s = new Shader(NinjaRace.Shaders.Test);
        //Texture t = Program.font.MakeTexture("ASD");
        //Draw.Scale((double)t.Width / (double)t.Height, 1);
        //s.SetTexture("texture", t);
        //s.SetVec2("size", new Vec2(t.Width, t.Height));
        //s.Render();
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