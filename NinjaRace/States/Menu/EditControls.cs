using VitPro;
using VitPro.Engine;
using VitPro.Engine.UI;
using System;

class EditControls : Menu
{
    public Button P1Left,
        P1Right,
        P1Down,
        P1Jump,
        P1Bonus,
        P1Sword,
        P2Left,
        P2Right,
        P2Down,
        P2Jump,
        P2Bonus,
        P2Sword;
    public EditControls()
    {
        Label p1 = new Label("PLAYER 1", 20);
        Label p2 = new Label("PLAYER 2", 20);
        Label left = new Label("LEFT", 20);
        Label right = new Label("RIGHT", 20);
        Label down = new Label("DOWN", 20);
        Label jump = new Label("JUMP", 20);
        Label bonus = new Label("BONUS", 20);
        Label sword = new Label("SWORD", 20);

        ElementList list = new ElementList();
        list.Add(left);
        list.Add(right);
        list.Add(down);
        list.Add(jump);
        list.Add(bonus);
        list.Add(sword);
        list.Anchor = new Vec2(0.25, 0.6);
        list.Horizontal = false;
        list.Visit((Element e) => { e.FixedWidth = 150; });

        P1Left = new Button(Program.Settings.P1Left.ToString(), () => { Program.Manager.PushState(new KeyPress(SetP1Left)); }, 20, 100);
        P1Right = new Button(Program.Settings.P1Right.ToString(), () => { Program.Manager.PushState(new KeyPress(SetP1Right)); }, 20, 100);
        P1Down = new Button(Program.Settings.P1Down.ToString(), () => { Program.Manager.PushState(new KeyPress(SetP1Down)); }, 20, 100);
        P1Jump = new Button(Program.Settings.P1Jump.ToString(), () => { Program.Manager.PushState(new KeyPress(SetP1Jump)); }, 20, 100);
        P1Bonus = new Button(Program.Settings.P1Bonus.ToString(), () => { Program.Manager.PushState(new KeyPress(SetP1Bonus)); }, 20, 100);
        P1Sword = new Button(Program.Settings.P1Sword.ToString(), () => { Program.Manager.PushState(new KeyPress(SetP1Sword)); }, 20, 100);
        P2Left = new Button(Program.Settings.P2Left.ToString(), () => { Program.Manager.PushState(new KeyPress(SetP2Left)); }, 20, 200);
        P2Right = new Button(Program.Settings.P2Right.ToString(), () => { Program.Manager.PushState(new KeyPress(SetP2Right)); }, 20, 200);
        P2Down = new Button(Program.Settings.P2Down.ToString(), () => { Program.Manager.PushState(new KeyPress(SetP2Down)); }, 20, 200);
        P2Jump = new Button(Program.Settings.P2Jump.ToString(), () => { Program.Manager.PushState(new KeyPress(SetP2Jump)); }, 20, 200);
        P2Bonus = new Button(Program.Settings.P2Bonus.ToString(), () => { Program.Manager.PushState(new KeyPress(SetP2Bonus)); }, 20, 200);
        P2Sword = new Button(Program.Settings.P2Sword.ToString(), () => { Program.Manager.PushState(new KeyPress(SetP2Sword)); }, 20, 200);

        ElementList p1list = new ElementList();
        p1list.Add(P1Left);
        p1list.Add(P1Right);
        p1list.Add(P1Down);
        p1list.Add(P1Jump);
        p1list.Add(P1Bonus);
        p1list.Add(P1Sword);
        p1list.Anchor = new Vec2(0.55, 0.6);
        p1list.Horizontal = false;
        p1list.Visit((Element e) => { e.FixedWidth = 100; });

        ElementList p2list = new ElementList();
        p2list.Add(P2Left);
        p2list.Add(P2Right);
        p2list.Add(P2Down);
        p2list.Add(P2Jump);
        p2list.Add(P2Bonus);
        p2list.Add(P2Sword);
        p2list.Anchor = new Vec2(0.8, 0.6);
        p2list.Horizontal = false;
        p2list.Visit((Element e) => { e.FixedWidth = 100; });

        p1.Anchor = new Vec2(0.55, 0.9);
        p2.Anchor = new Vec2(0.8, 0.9);

        Button Done = new Button("DONE", () =>
        {
            Close();
            Program.Manager.NextState = new MainMenu();
        }, 50, 150);
        Done.Anchor = new Vec2(0.5, 0.2);
        AddElement(Done);

        AddElement(list);
        AddElement(p1);
        AddElement(p2);
        AddElement(p1list);
        AddElement(p2list);
    }

    void SetP1Left(Key key)
    {
        P1Left.Text = key.ToString();
        Program.Settings.P1Left = key;
    }
    void SetP1Right(Key key)
    {
        P1Right.Text = key.ToString();
        Program.Settings.P1Right = key;
    }
    void SetP1Down(Key key)
    {
        P1Down.Text = key.ToString();
        Program.Settings.P1Down = key;
    }
    void SetP1Jump(Key key)
    {
        P1Jump.Text = key.ToString();
        Program.Settings.P1Jump = key;
    }
    void SetP1Bonus(Key key)
    {
        P1Bonus.Text = key.ToString();
        Program.Settings.P1Bonus = key;
    }
    void SetP1Sword(Key key)
    {
        P1Sword.Text = key.ToString();
        Program.Settings.P1Sword = key;
    }
    void SetP2Left(Key key)
    {
        P2Left.Text = key.ToString();
        Program.Settings.P2Left = key;
    }
    void SetP2Right(Key key)
    {
        P2Right.Text = key.ToString();
        Program.Settings.P2Right = key;
    }
    void SetP2Down(Key key)
    {
        P2Down.Text = key.ToString();
        Program.Settings.P2Down = key;
    }
    void SetP2Jump(Key key)
    {
        P2Jump.Text = key.ToString();
        Program.Settings.P2Jump = key;
    }
    void SetP2Bonus(Key key)
    {
        P2Bonus.Text = key.ToString();
        Program.Settings.P2Bonus = key;
    }
    void SetP2Sword(Key key)
    {
        P2Sword.Text = key.ToString();
        Program.Settings.P2Sword = key;
    }
}