using VitPro;
using VitPro.Engine;
using UI = VitPro.Engine.UI;

class Label : UI.Label
{
    public Label(string text, double size)
        : base(text, size)
    {
        BackgroundColor = new Color(0.2, 0.2, 0.2, 0);
        Font = Program.font;
    }

    public Label(string text, double size, double width)
        : this(text, size)
    {
        FixedWidth = width;
    }
}