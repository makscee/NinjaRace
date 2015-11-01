using VitPro;
using VitPro.Engine;
using System;

class CreateLevel : Menu
{
    TextInput xInput = new TextInput(200);
    TextInput yInput = new TextInput(200);
    TextInput Name = new TextInput(200);

    public CreateLevel(bool showdown, string name = null)
    {
        Label title = new Label("CREATE NEW " + (showdown ? "SHOWDOWN" : "GAME") + " LEVEL", 40);
        title.Anchor = new Vec2(0.5, 0.9);
        Frame.Add(title);

        if (name == null)
        {
            Name.Anchor = new Vec2(0.6, 0.6);
            Frame.Add(Name);
        }
        else 
        {
            Label nameV = new Label(name, 30);
            nameV.Anchor = new Vec2(0.5, 0.6);
            Frame.Add(nameV);
        }

        Label nameL = new Label("NAME:", 30);
        nameL.Anchor = new Vec2(0.3, 0.6);
        Frame.Add(nameL);

        xInput.Anchor = new Vec2(0.6, 0.4);
        Frame.Add(xInput);

        Label xSize = new Label("X SIZE:", 30);
        xSize.Anchor = new Vec2(0.3, 0.4);
        Frame.Add(xSize);

        yInput.Anchor = new Vec2(0.6, 0.3);
        Frame.Add(yInput);

        Label ySize = new Label("Y SIZE:", 30);
        ySize.Anchor = new Vec2(0.3, 0.3);
        Frame.Add(ySize);

        Button done = new Button("DONE",
            () =>
            {
                this.Close();
                if (!showdown)
                    Program.Manager.PushState(new CreateLevel(true, Name.Value));
                Program.Manager.NextState = 
                    new LevelEditor(int.Parse(xInput.Value), int.Parse(yInput.Value), 
                        showdown ? name : Name.Value, showdown);
            }, 50, 120);
        done.Anchor = new Vec2(0.5, 0.1);
        Frame.Add(done);
    }
}