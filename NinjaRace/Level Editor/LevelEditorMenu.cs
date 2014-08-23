using VitPro;
using VitPro.Engine;
using System;

class LevelEditorMenu : Menu
{
    int sizex = 10, sizey = 10;
    public LevelEditorMenu()
    {
        buttons.Add(new Button(new Vec2(0, -80), new Vec2(80, 20))
            .SetName("DONE")
            .SetAction(() => Program.Manager.PushState(new LevelEditor(sizex, sizey))));
        fields.Add(new EnterField(new Vec2(0, 20), new Vec2(100, 20), 5)
            .SetName("WIDTH"));

        buttons.Refresh();
        fields.Refresh();
    }


}