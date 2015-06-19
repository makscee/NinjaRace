using VitPro;
using VitPro.Engine;
using System;

class LevelEditorMenu : Menu
{
    TextInput xInput = new TextInput(200);
    TextInput yInput = new TextInput(200);
    TextInput name = new TextInput(200);
    CheckBox showdown = new CheckBox(20);
    public LevelEditorMenu()
    {
        name.Anchor = new Vec2(0.6, 0.8);
        name.Value = "DEFAULT";
        Frame.Add(name);

        Label nameL = new Label("NAME:", 30);
        nameL.Anchor = new Vec2(0.3, 0.8);
        Frame.Add(nameL);

        xInput.Anchor = new Vec2(0.6, 0.7);
        Frame.Add(xInput);

        Label xSize = new Label("X SIZE:", 30);
        xSize.Anchor = new Vec2(0.3, 0.7);
        Frame.Add(xSize);

        yInput.Anchor = new Vec2(0.6, 0.6);
        Frame.Add(yInput);

        Label ySize = new Label("Y SIZE:", 30);
        ySize.Anchor = new Vec2(0.3, 0.6);
        Frame.Add(ySize);
        Button edit = new Button("EDIT EXISTING", () => { this.Close(); Program.Manager.PushState(new LevelEditor(name.Value, showdown.Checked)); }, 50);
        edit.Anchor = new Vec2(0.5, 0.4);
        Frame.Add(edit);

        Button done = new Button("DONE",
            () => { this.Close(); Program.Manager.PushState(GetState()); }, 50);
        done.Anchor = new Vec2(0.5, 0.2);
        Frame.Add(done);

        showdown.Anchor = new Vec2(0.8, 0.8);
        Frame.Add(showdown);
    }

    LevelEditor GetState()
    {
        return new LevelEditor(int.Parse(xInput.Value), int.Parse(yInput.Value), name.Value, showdown.Checked);
    }
}