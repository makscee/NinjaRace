using VitPro;
using VitPro.Engine;
using System;

class Button : IRenderable
{
    private Action action;
    private Vec2 position, size;
    private Texture text;
    private Color backGroundColor = Color.Gray, textColor = Color.Green;

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

    public Button SetText(string text)
    {
        this.text = Program.font.MakeTexture(text);
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
        if(action != null)
            action();
    }

    public void Render()
    {
        Color t = Hit() ? new Color(Math.Min(backGroundColor.R * 2, 1d), Math.Min(backGroundColor.G * 2, 1d), Math.Min(backGroundColor.B * 2, 1d)) :
            backGroundColor;
        Draw.Rect(position - size, position + size, t);
        Draw.Save();
        Draw.Color(textColor);
        Draw.Translate(position);
        Draw.Scale((double)text.Width / (double)text.Height, 1);
        Draw.Scale(size.Y - 4);
        Draw.Align(0.5, 0.5);
        text.Render();
        Draw.Load();
    }

    private bool Hit()
    {
        Vec2 pos = Mouse.Position;
        pos += new Vec2(0, -240);
        pos -= new Vec2(320, 0);
        pos /= 2;
        if (pos.X < position.X + size.X && pos.X > position.X - size.X && pos.Y < position.Y + size.Y && pos.Y > position.Y - size.Y)
            return true;
        return false;
    }
}