using VitPro;
using VitPro.Engine;
using System;

class DisplayField : IRenderable
{
    private Vec2 position, size;
    private Texture name;
    private Color backGroundColor = new Color(0.2, 0.2, 0.2), textColor = Color.White;

    public DisplayField(Vec2 position, Vec2 size)
    {
        this.position = position;
        this.size = size;
    }
    
    public DisplayField SetName(string name)
    {
        this.name = Program.font.MakeTexture(name);
        return this;
    }

    public DisplayField SetColors(Color background, Color text)
    {
        backGroundColor = background;
        textColor = text;
        return this;
    }

    public void Render()
    {
        Draw.Rect(position - size, position + size, backGroundColor);
        Draw.Save();
        Draw.Color(textColor);
        Draw.Translate(position);
        Draw.Scale((double)name.Width / (double)name.Height, 1);
        Draw.Scale(size.Y);
        Draw.Align(0.5, 0.5);
        name.Render();
        Draw.Load();
    }
}