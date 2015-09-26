using VitPro;
using VitPro.Engine;
using UI = VitPro.Engine.UI;

class TextInput : UI.TextInput
{
    public TextInput(double width)
        : base(width)
    {
        BackgroundColor = new Color(0.2, 0.2, 0.2);
        TextColor = Color.White;
        Font = Program.font;
        Padding = 0.2;
        TextAlign = 0;
        FocusedBorderColor = Color.White;
        OnFocus += () => BackgroundColor = new Color(0.3, 0.25, 0.2);
        OnLoseFocus += () => BackgroundColor = new Color(0.2, 0.2, 0.2);
    }

    public override void CharInput(char c)
    {
        c = c.ToString().ToUpper().ToCharArray()[0];
        base.CharInput(c);
    }
}