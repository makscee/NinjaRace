using VitPro;
using VitPro.Engine;
using System;

class LevelEditorMenu : Menu
{
    EnterField xField, yField;
    public LevelEditorMenu()
    {
        buttons.Add(new Button(new Vec2(0, -30), new Vec2(80, 20))
            .SetName("DONE")
            .SetAction(() => { this.Close(); Program.Manager.PushState(GetState()); }));
        buttons.Add(new Button(new Vec2(0, -80), new Vec2(80, 20))
            .SetName("EDIT EXISTING")
            .SetAction(() => { this.Close(); Program.Manager.PushState(new LevelEditor()); }));
        yField = new EnterField(new Vec2(0, 60), new Vec2(100, 20), 5)
            .SetName("WIDTH")
            .SetDefault("0");
        xField = new EnterField(new Vec2(0, 20), new Vec2(100, 20), 5)
            .SetName("HEGHT")
            .SetDefault("0");
        fields.Add(xField);
        fields.Add(yField);

        buttons.Refresh();
        fields.Refresh();
    }

    State GetState()
    {
        int sizex = int.Parse(xField.GetText());
        int sizey = int.Parse(yField.GetText());
        return new LevelEditor(sizex + 1, sizey + 1);
    }
}