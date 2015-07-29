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
        () => { Program.Manager.NextState = new CreateLevel(false); }, 50);
    Button Edit = new Button("EDIT LEVEL",
        () => { Program.Manager.NextState = new EditLevel(); }, 50);
    public LevelEditorMenu()
    {
        Create.Anchor = new Vec2(0.5, 0.8);
        AddExpandElement(Create, 400);
        Frame.Add(Create);
        Edit.Anchor = new Vec2(0.5, 0.4);
        AddExpandElement(Edit, 400);
        Frame.Add(Edit);
    }
}