using VitPro;
using VitPro.Engine;
using System;

class LevelEditorHelp : VitPro.Engine.UI.State
{
    public LevelEditorHelp()
    {
        VitPro.Engine.UI.ElementList list = new VitPro.Engine.UI.ElementList();
        list.Anchor = new Vec2(0.5, 0.5);
        list.Horizontal = false;
        list.Add(new Label("CHOSE TILE USING \"Q\" AND \"E\" BUTTONS", 25));
        list.Add(new Label("PUT TILE USING LEFT MOUSE BUTTON", 25));
        list.Add(new Label("MOVE CAMERA BY HOLDING RIGHT MOUSE BUTTON", 25));
        list.Add(new Label("CONTROL CAMERA FOV VIA MOUSE WHEEL", 25));
        list.Add(new Label("USE CHECKBOX ON TOP LEFT TO ENALBE MIRROR MODE", 25));
        list.Add(new Label("TO PUT SAW: HOLD MOUSE ON FIRST POSITION, RELEASE ON SECOND", 18));
        list.Spacing = 35;
        Frame.Add(list);
        BackgroundColor = Color.Black;
    }
}