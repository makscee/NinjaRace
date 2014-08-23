using VitPro;
using VitPro.Engine;
using System;

class Menu : State
{
    protected Group<Button> buttons = new Group<Button>();
    private Camera cam = new Camera(240);

    public override void Render()
    {
        cam.Apply();
        Draw.Clear(Color.Black);
        buttons.Render();
    }

    public override void MouseDown(MouseButton button, Vec2 pos)
    {
        if (button == MouseButton.Left)
            foreach (var a in buttons)
                a.Click();
    }
}