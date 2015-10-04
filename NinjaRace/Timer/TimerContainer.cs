using System;
using System.Collections.Generic;
using VitPro.Engine;
using VitPro;

static class TimerContainer
{
    public static Group<Timer> Timers = new Group<Timer>();

    public static void Update(double dt)
    {
        Timers.Update(dt);
        Timers.Refresh();
    }

    public static void Clear()
    {
        foreach (var a in Timers)
            a.Complete();
        Timers.Refresh();
    }
}
