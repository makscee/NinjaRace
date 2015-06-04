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
        Shader s = new Shader(NinjaRace.Shaders.Test);
        this.name = Program.font.MakeTexture(name);
		RenderState.Push();
		RenderState.Set("size", new Vec2(this.name.Width, this.name.Height));
		RenderState.Set("doX", 0);
        this.name.ApplyShader(s);
		RenderState.Set("doX", 1);
        this.name.ApplyShader(s);
		RenderState.Pop();
        return this;
    }

    public DisplayField SetColors(Color background, Color text)
    {
        backGroundColor = background;
        textColor = text;
        return this;
    }

    Vec2 offset;
    public DisplayField SetTextOffset(Vec2 v)
    {
        offset = v;
        return this;
    }
    public DisplayField SetTextOffset(double horizontal, double vertical)
    {
        offset = new Vec2(size.X * horizontal, size.Y * vertical);
        return this;
    }

    double textScale = 20;
    public DisplayField SetTextScale(double s)
    {
        textScale = s;
        return this;
    }

    Vec2 align = new Vec2(0.5, 0.5);
    public DisplayField SetTextAlign(Vec2 v)
    {
        align = v / 2 + new Vec2(0.5, 0.5);
        return this;
    }
    public DisplayField SetTextAlign(double horizontal, double vertical)
    {
        align = new Vec2(horizontal / 2 + 0.5, vertical / 2 + 0.5);
        return this;
    }

    public void Render()
    {
        Draw.Rect(position - size, position + size, backGroundColor);
		RenderState.Push();
		RenderState.Translate(offset);
		RenderState.Color = textColor;
		RenderState.Translate(position);
		RenderState.Scale((double)name.Width / (double)name.Height, 1);
		RenderState.Scale(textScale);
		RenderState.Origin(align);
        name.Render();
		RenderState.Pop();
    }

    public DisplayField SetTextFromTheLeft()
    {
        SetTextAlign(-0.9, 0);
        SetTextOffset(-0.9, 0);
        return this;
    }
}