using VitPro;
using VitPro.Engine;
using System;

class Button : IRenderable
{
    private Action action;
    private Vec2 position, size;
    private Texture name;
    private Color backGroundColor = new Color(0.7, 0.7, 0.7), textColor = Color.Green;

    public Button(Vec2 position, Vec2 size)
    {
        this.position = position;
        this.size = size;
    }

    public Button SetAction(Action action)
    {
        this.action = action;
        return this;
    }

    public Button SetName(string name)
    {
        this.name = Program.font.MakeTexture(name);
        return this;
    }

    public Button SetColors(Color background, Color text)
    {
        backGroundColor = background;
        textColor = text;
        return this;
    }

    public void Click()
    {
        if(action != null && Hit())
            action();
    }

    public void Render()
    {
        Color t = Hit() ? 
            new Color(Math.Min(backGroundColor.R * 1.5, 1d), Math.Min(backGroundColor.G * 1.5, 1d), Math.Min(backGroundColor.B * 1.5, 1d))
            : backGroundColor;
        Draw.Rect(position - size, position + size, t);
        Draw.Save();
        Draw.Color(textColor);
        Draw.Translate(position);
        Draw.Scale((double)name.Width / (double)name.Height, 1);
        Draw.Scale(size.Y - 2);
        Draw.Align(0.5, 0.5);
        name.Render();
        Draw.Load();
    }

    private bool Hit()
    {
        Vec2 pos = Program.MousePosition();
        if (pos.X < position.X + size.X && pos.X > position.X - size.X && pos.Y < position.Y + size.Y && pos.Y > position.Y - size.Y)
            return true;
        return false;
    }
}