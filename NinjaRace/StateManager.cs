using VitPro;
using VitPro.Engine;
using System;
using System.Diagnostics;

class MyManager : State.Manager
{
    public MyManager(params State[] states)
        : base(states)
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

    State Previous, Current;

    void DefaultRender()
    {
		RenderState.Push();
		RenderState.Push();
		RenderState.Scale(2 + t);
		RenderState.Origin(0.5, 0.5);
        tex.Render();
		RenderState.Pop();
		RenderState.Scale(3 - t);
		RenderState.Origin(0.5, 0.5);
        if (back != null)
        {
			RenderState.Color = new Color(1, 1, 1, t);
            back.Render();
        }
		RenderState.Pop();
    }

    double fps = 0;
    Stopwatch sw = new Stopwatch();

    public override void Render()
    {
		if (tex == null || tex.Width != RenderState.Width || tex.Height != RenderState.Height)
			tex = new Texture(RenderState.Width, RenderState.Height);
		RenderState.BeginTexture(tex);
        base.Render();
		RenderState.EndTexture();
        tex.RemoveAlpha();
        DefaultRender();
        //RenderState.Push();
        //Texture t = Program.font.MakeTexture(Math.Truncate(fps).ToString());
        //RenderState.Color = (Color.Yellow);
        //Draw.Texture(t, new Vec2(0.85, 0.85) - new Vec2(0.1 * t.Width / t.Height / 2, 0.1), new Vec2(0.85, 0.85) + new Vec2(0.1 * t.Width / t.Height / 2, 0.1));
        //t.Dispose();
        //RenderState.Pop();
    }
}