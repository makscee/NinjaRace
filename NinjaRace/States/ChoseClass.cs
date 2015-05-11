using VitPro;
using System;
using VitPro.Engine;
using System.Collections.Generic;

class ChoseClass : Menu
{
    DisplayField Mode;
    public ChoseClass()
    {
        //Mode = new DisplayField(new Vec2(0, 20), new Vec2(50, 20))
        //    .SetName("Quaker")
        //    .SetColors(Color.Black, Color.Orange);
        //buttons.Add(new Button(new Vec2(90, 20), new Vec2(20, 20))
        //    .SetName(">")
        //    .SetAction(() => multiplayer = !multiplayer));
        buttons.Add(new Button(new Vec2(0, -50), new Vec2(80, 20))
            .SetName("START")
            .SetAction(() => { this.Close(); Program.Manager.PushState(new Game()); }));
        //dfields.Add(Mode);

        dfields.Refresh();
        buttons.Refresh();
    }
}