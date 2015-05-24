using VitPro;
using VitPro.Engine;
using System;

class LevelEditorMenu : Menu
{
    EnterField xField, yField, name;
    public LevelEditorMenu()
    {
        AddButton(new Button(new Vec2(0, -30), new Vec2(80, 20))
            .SetName("DONE")
            .SetAction(() => { this.Close(); Program.Manager.PushState(GetState()); }));
        AddButton(new Button(new Vec2(0, -80), new Vec2(80, 20))
            .SetName("EDIT EXISTING")
            .SetAction(() => { this.Close(); Program.Manager.PushState(new LevelEditor(name.GetText())); }));
        yField = new EnterField(new Vec2(0, 50), new Vec2(100, 15), 5)
            .SetName("WIDTH")
            .SetDefault("0");
        xField = new EnterField(new Vec2(0, 20), new Vec2(100, 15), 5)
            .SetName("HEGHT")
            .SetDefault("0");
        name = new EnterField(new Vec2(0, 80), new Vec2(100, 15), 9)
            .SetName("NAME")
            .SetDefault("DEFAULT");
        
        AddEField(xField);
        AddEField(yField);
        AddEField(name);
    }

    State GetState()
    {
        int sizex = int.Parse(xField.GetText());
        int sizey = int.Parse(yField.GetText());
        return new LevelEditor(sizex + 1, sizey + 1, name.GetText());
    }
}