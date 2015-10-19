using System;
using VitPro.Engine;

class Timer : IUpdateable
{
    double elapsed = 0, elapsedPart = 0, life, period;
    Action Callback = new Action(() => {});
    bool Alive = true;

    public Timer(double life, Action callback)
    {
        this.life = life;
        period = life;
        this.Callback += callback;
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
        Callback += action;
    }

    public double Elapsed { get { return elapsed; } }

    public void Stop()
    {
        Alive = false;
    }

    public void Start()
    {
        Alive = true;
    }

    public bool IsDone
    {
        get { return elapsed >= life; }
    }

    public void Update(double dt)
    {
        if (!Alive)
            return;
        elapsed += dt;
        elapsedPart += dt;
        if (elapsedPart > period)
        {
            Callback.Invoke();
            elapsedPart = 0;
        }
        if (IsDone)
        {
            TimerContainer.Timers.Remove(this);
            return;
        }
    }

    public void Complete()
    {
        if (!IsDone)
            Callback.Invoke();
        Stop();
        elapsed = life;
        TimerContainer.Timers.Remove(this);
        return;
    }

    public void Drop()
    {
        TimerContainer.Timers.Remove(this);
        Stop();
        elapsed = life;
        return;
    }
}