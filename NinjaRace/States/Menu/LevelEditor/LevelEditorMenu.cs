using VitPro;
using VitPro.Engine;
using System;

class LevelEditorMenu : Menu
{
    TextInput xInput = new TextInput(200);
    TextInput yInput = new TextInput(200);
    TextInput name = new TextInput(200);
    CheckBox showdown = new CheckBox(20);

    Button Create = new Button("CREATE LEVEL",
        () => { Program.Manager.NextState = new CreateLevel(false); }, 70);
    Button Edit = new Button("EDIT LEVEL",
        () => { Program.Manager.NextState = new EditLevel(); }, 70);
    public LevelEditorMenu()
    {
        Create.Anchor = new Vec2(0.5, 0.7);
        Frame.Add(Create);
        Edit.Anchor = new Vec2(0.5, 0.3);
        Frame.Add(Edit);
    }
}