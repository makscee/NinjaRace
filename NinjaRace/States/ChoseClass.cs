using VitPro;
using System;
using VitPro.Engine;

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
        buttons.Add(new Button(new Vec2(-90, 60), new Vec2(20, 20))
            .SetName("<")
            .SetAction(() => Player1(false)));
        buttons.Add(new Button(new Vec2(90, 60), new Vec2(20, 20))
            .SetName(">")
            .SetAction(() => Player1(true)));
        buttons.Add(new Button(new Vec2(-90, 0), new Vec2(20, 20))
            .SetName("<")
            .SetAction(() => Player2(false)));
        buttons.Add(new Button(new Vec2(90, 0), new Vec2(20, 20))
            .SetName(">")
            .SetAction(() => Player2(true)));
        buttons.Add(new Button(new Vec2(0, -50), new Vec2(80, 20))
            .SetName("START")
            .SetAction(() => { this.Close(); Program.Manager.PushState(GetState()); }));
        dfields.Add(player1field);
        dfields.Add(player2field);

        dfields.Refresh();
        buttons.Refresh();
    }

    State GetState()
    {
        player1.SetControls(new ControllerPlayer1());
        if (player2 != null)
            player2.SetControls(new ControllerPlayer2());
        return new Game(player1, player2);
    }

    void Player1(bool r)
    {
        if (r)
        {
            if (player1 is Quaker)
            {
                player1 = new Dasher();
                player1field.SetName("Dasher")
                    .SetColors(Color.Black, Color.Magenta);
                return;
            }

            if (player1 is Dasher)
            {
                player1 = new Quaker();
                player1field.SetName("Quaker")
                    .SetColors(Color.Black, Color.Orange);
                return;
            }
        }
        else
        {
            if (player1 is Quaker)
            {
                player1 = new Dasher();
                player1field.SetName("Dasher")
                    .SetColors(Color.Black, Color.Magenta);
                return;
            }

            if (player1 is Dasher)
            {
                player1 = new Quaker();
                player1field.SetName("Quaker")
                    .SetColors(Color.Black, Color.Orange);
                return;
            }
        }
    }

    void Player2(bool r)
    {
        if (r)
        {
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
        else
        {
            if (player2 == null)
            {
                player2 = new Dasher();
                player2field.SetName("Dasher")
                    .SetColors(Color.Black, Color.Magenta);
                return;
            }
            if (player2 is Quaker)
            {
                player2 = null;
                player2field.SetName("None")
                    .SetColors(Color.Black, Color.Gray);
                return;
            }

            if (player2 is Dasher)
            {
                player2 = new Quaker();
                player2field.SetName("Quaker")
                    .SetColors(Color.Black, Color.Orange);
                return;
            }
        }
    }
}