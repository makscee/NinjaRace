using VitPro;
using VitPro.Engine;
using System;
using System.Diagnostics;

class MyManager : StateManager
{
    public MyManager(State a, params IState[] states)
        : base(a, states)
    {
        sw.Start();
    }
    double t = 1;

    public override void Update(double dt)
    {
        if (sw.Elapsed.Milliseconds > 500)
        {
            fps = 1 / dt;
            sw.Restart();
        }
        if (t == 0)
            base.Update(dt);
        t -= 5 * dt;
        if (t < 0)
            t = 0;
    }

    public override void StateChanged()
    {
        base.StateChanged();
        t = 1;
        if (tex != null)
        {
            back = tex.Copy();
        }
        Previous = Current;
        Current = CurrentState;
    }

    Texture tex = null, back = null;

    IState Previous, Current;

    void DefaultRender()
    {
        Draw.Save();
        Draw.Save();
        Draw.Scale(2 + t);
        Draw.Align(0.5, 0.5);
        tex.Render();
        Draw.Load();
        Draw.Scale(3 - t);
        Draw.Align(0.5, 0.5);
        if (back != null)
        {
            Draw.Color(1, 1, 1, t);
            back.Render();
        }
        Draw.Load();
    }

    double fps = 0;
    Stopwatch sw = new Stopwatch();

    public override void Render()
    {
        if (tex == null || tex.Width != Draw.Width || tex.Height != Draw.Height)
            tex = new Texture(Draw.Width, Draw.Height);
        Draw.BeginTexture(tex);
        base.Render();
        Draw.EndTexture();
        tex.RemoveAlpha();
        DefaultRender();
        Draw.Save();
        Texture t = Program.font.MakeTexture(Math.Truncate(fps).ToString());
        Draw.Color(Color.Yellow);
        t.RenderToPosAndSize(new Vec2(0.85, 0.85), new Vec2(0.1 * t.Width / t.Height / 2, 0.1));
        t.Dispose();
        Draw.Load();
    }
}