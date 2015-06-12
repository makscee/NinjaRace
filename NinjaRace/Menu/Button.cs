using VitPro;
using VitPro.Engine;
using UI = VitPro.Engine.UI;
using System;

class Button : UI.Button
{
    public Button(string text, Action action, double size) : base(text, action, size) 
    {
        UnhoveredColor = new Color(0.2, 0.2, 0.2);
        HoveredColor = new Color(0.3, 0.3, 0.3);
        TextColor = Color.White;
        Font = Program.font;
        Padding = 0.2;
        
        BorderWidth = 0;
    }

}