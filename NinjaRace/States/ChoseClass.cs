using VitPro;
using System;
using VitPro.Engine;
using System.Collections.Generic;

class ChoseClass : Menu
{
    Player player1 = new Quaker(), player2 = null;
    DisplayField player1field, player2field;
    public ChoseClass()
    {
        player1field = new DisplayField(new Vec2(0, 60), new Vec2(50, 20))
            .SetName("Quaker")
            .SetColors(Color.Black, Color.Orange);
        player2field = new DisplayField(new Vec2(0, 0), new Vec2(50, 20))
            .SetName("None")
            .SetColors(Color.Black, Color.Gray);
        buttons.Add(new Button(new Vec2(90, 60), new Vec2(20, 20))
            .SetName(">")
            .SetAction(() => Player1()));
        buttons.Add(new Button(new Vec2(90, 0), new Vec2(20, 20))
            .SetName(">")
            .SetAction(() => Player2()));
        buttons.Add(new Button(new Vec2(0, -50), new Vec2(80, 20))
            .SetName("START")
            .SetAction(() => { this.Close(); Program.Manager.PushState(GetState()); }));
        dfields.Add(player1field);
        dfields.Add(player2field);

        dfields.Refresh();
        buttons.Refresh();

        player1enum = classes.GetEnumerator();
        player1enum.MoveNext();
        player2enum = classes.GetEnumerator();
    }

    State GetState()
    {
        Type class1 = Type.GetType(player1enum.Current.Item1);
        player1 = (Player)class1.GetConstructor(new Type[] { }).Invoke(new object[] { });
        if (player2enum.Current != null)
        {
            Type class2 = Type.GetType(player2enum.Current.Item1);
            player2 = (Player)class2.GetConstructor(new Type[] { }).Invoke(new object[] { });
        }

        player1.SetControls(new ControllerPlayer1());
        if (player2 != null)
            player2.SetControls(new ControllerPlayer2());
        Program.InitWorld1(player1);
        Program.InitWorld2(player2);
        return new Game();
    }

    List<Tuple<string, Color>> classes = new List<Tuple<string, Color>>() 
    {
        new Tuple<string, Color>("Quaker", Color.Orange),
        new Tuple<string, Color>("Dasher", Color.Magenta)
    };

    List<Tuple<string, Color>>.Enumerator player1enum;
    void Player1()
    {
        if (player1enum.MoveNext())
        {
            player1field.SetName(player1enum.Current.Item1)
                .SetColors(Color.Black, player1enum.Current.Item2);
        }
        else
        {
            player1enum = classes.GetEnumerator();
            Player1();
        }
    }

    List<Tuple<string, Color>>.Enumerator player2enum;
    void Player2()
    {
        if (player2enum.MoveNext())
        {
            player2field.SetName(player1enum.Current.Item1)
                .SetColors(Color.Black, player1enum.Current.Item2);
        }
        else
        {
            player2enum = classes.GetEnumerator();
            player2field.SetName("None")
                .SetColors(Color.Black, Color.Gray);
        }

        if (player2 == null)
        {
            player2 = new Quaker();
            player2field.SetName("Quaker")
                .SetColors(Color.Black, Color.Orange);
            return;
        }
        if (player2 is Quaker)
        {
            player2 = new Dasher();
            player2field.SetName("Dasher")
                .SetColors(Color.Black, Color.Magenta);
            return;
        }

        if (player2 is Dasher)
        {
            player2 = null;
            player2field.SetName("None")
                .SetColors(Color.Black, Color.Gray);
            return;
        }
    }
}