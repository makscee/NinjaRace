using System;
using VitPro.Engine;

class Timer : IUpdateable
{
    double elapsed = 0, elapsedPart = 0, life, period;
    Action callback = new Action(() => {});
    bool Alive = true;

    public Timer(double life, Action callback)
    {
        this.life = life;
        period = life;
        this.callback += callback;
        TimerContainer.Timers.Add(this);
    }

    public Timer(double life, double period, Action callback)
        : this(life, callback)
    {
        this.period = period;
    }

    public Timer(int count, double period, Action callback)
        : this(count * period, period, callback) { }

    public void AddCallback(Action action)
    {
        callback += action;
    }

    public double Elapsed { get { return elapsed; } }

    public void Stop()
    {
        Alive = true;
    }

    public void Start()
    {
        Alive = false;
    }

    public bool IsDone()
    {
        return elapsed > life;
    }

    public void Update(double dt)
    {
        if (!Alive)
            return;
        if (elapsed > life)
        {
            TimerContainer.Timers.Remove(this);
            return;
        }
        elapsed += dt;
        elapsedPart += dt;
        if (elapsedPart > period)
        {
            callback.Invoke();
            elapsedPart = 0;
        }
    }
}