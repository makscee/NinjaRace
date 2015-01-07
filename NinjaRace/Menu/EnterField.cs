using VitPro;
using VitPro.Engine;
using System;

class EnterField : IRenderable
{
    private string text = "", defaultText;
    private bool focused = false;
    private Vec2 position, size;
    private int lim;
    private Color backGroundColor = new Color(0.2, 0.2, 0.2), textColor = Color.White;
    private Texture textTex, nameTex;

    public EnterField(Vec2 position, Vec2 size, int lim)
    {
        this.position = position;
        this.size = size;
        this.lim = lim;
        RefreshTexture();
    }

    public EnterField SetName(string name)
    {
        this.nameTex = Program.font.MakeTexture(name);
        return this;
    }

    public EnterField SetDefault(string defaultText)
    {
        this.defaultText = defaultText;
        text = defaultText;
        RefreshTexture();
        return this;
    }

    public EnterField SetColors(Color background, Color text)
    {
        backGroundColor = background;
        textColor = text;
        return this;
    }

    public string GetText() { return text; }

    public void Click()
    {
        if (Hit())
            GainFocus();
        else LoseFocus();
    }

    private void GainFocus()
    {
        if (text == defaultText)
            text = "";
        focused = true;
        RefreshTexture();
    }

    private void LoseFocus()
    {
        focused = false;
        if (text == "")
            text = defaultText;
        RefreshTexture();
    }

    public virtual void Enter(Key key)
    {
        if (!focused)
            return;
        if (key == Key.Enter)
        {
            LoseFocus();
            return;
        }
        if (key == Key.BackSpace)
        {
            if (text.Length == 0)
                return;
            text = text.Substring(0, text.Length - 1);
            RefreshTexture();
            return;
        }
        if (text.Length == lim)
            return;
        string t;
        if (key.ToString().Length > 1 && key.ToString()[0] == 'N')
            t = key.ToString()[key.ToString().Length - 1].ToString();
        else t = key.ToString()[0].ToString();
        text += t;
        RefreshTexture();
    }

    public void Render()
    {
        Color t = focused ? 
            new Color(Math.Min(backGroundColor.R * 1.5, 1d), Math.Min(backGroundColor.G * 1.5, 1d), Math.Min(backGroundColor.B * 1.5, 1d))
            : backGroundColor;
        Draw.Rect(position - size, position + size, t);
        Draw.Save();
        Draw.Color(textColor);
        Draw.Translate(position - new Vec2(size.X * 0.95, 0));
        Draw.Scale((double)nameTex.Width / (double)nameTex.Height, 1);
        Draw.Scale(size.Y - 4);
        Draw.Align(0, 0.5);
        nameTex.Render();
        Draw.Load();
        Draw.Save();
        Draw.Color(textColor);
        Draw.Translate(position + new Vec2(size.X * 0.95, 0));
        Draw.Scale((double)textTex.Width / (double)textTex.Height, 1);
        Draw.Scale(size.Y - 4);
        Draw.Align(1, 0.5);
        textTex.Render();
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

    private void RefreshTexture()
    {
        if (text.Length == 0)
            textTex = new Texture(0, 0);
        else textTex = Program.font.MakeTexture(text);
    }
}