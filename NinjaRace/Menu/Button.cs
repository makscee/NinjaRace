using VitPro;
using VitPro.Engine;
using System;

class Button : IRenderable
{
    private Action action;
    private Vec2 position, size;
    private Texture name;
    private Color backGroundColor = new Color(0.2, 0.2, 0.2), textColor = Color.White;

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

    double textScale = 20;
    public Button SetTextScale(double s)
    {
        textScale = s;
        return this;
    }

    Vec2 offset;
    public Button SetTextOffset(Vec2 v)
    {
        offset = v;
        return this;
    }
    public Button SetTextOffset(double horizontal, double vertical)
    {
        offset = new Vec2(size.X * horizontal, size.Y * vertical);
        return this;
    }

    public void Click()
    {
        if(action != null && Hit())
            action();
    }

    Vec2 align;
    public Button SetTextAlign(Vec2 v)
    {
        align = v / 2 + new Vec2(0.5, 0.5);
        return this;
    }
    public Button SetTextAlign(double horizontal, double vertical)
    {
        align = new Vec2(horizontal / 2 + 0.5, vertical / 2 + 0.5);
        return this;
    }

    public void Render()
    {
        Color t = Hit() ? 
            new Color(Math.Min(backGroundColor.R * 1.5, 1d), Math.Min(backGroundColor.G * 1.5, 1d), Math.Min(backGroundColor.B * 1.5, 1d))
            : backGroundColor;
        Draw.Rect(position - size, position + size, t);
        Draw.Save();
        Draw.Translate(offset);
        Draw.Color(textColor);
        Draw.Translate(position);
        Draw.Scale((double)name.Width / (double)name.Height, 1);
        Draw.Scale(textScale);
        Draw.Align(0.5 + align.X, 0.5 + align.Y);
        Shader s = new Shader(NinjaRace.Shaders.Test);
        s.SetTexture("texture", name);
        s.SetVec2("size", new Vec2(name.Width, name.Height));
        s.Render();
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