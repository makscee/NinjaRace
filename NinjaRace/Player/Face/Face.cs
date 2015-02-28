using VitPro;
using VitPro.Engine;
using System;

class Face : IRenderable, IUpdateable
{
    Player player;
    AnimatedTexture Current;
    AnimatedTexture Default;

    Group<AnimatedTexture> Fillers = new Group<AnimatedTexture>();
    double SinceLastFiller = 0;

    Vec2 Size = new Vec2(10, 10);
    Vec2 Position
    {
        get
        {
            return new Vec2(player.Position.X, player.Position.Y + player.Size.Y - Size.Y);
        }
    }

    public Face(Player player)
    {
        this.player = player;
        DefaultLoad.Load(this);
    }

    public void AddFiller(AnimatedTexture filler)
    {
        Fillers.Add(filler);
        Fillers.Refresh();
    }

    public void SetDefault(AnimatedTexture t)
    {
        Default = t;
    }

    public void Render()
    {
        UpdateCurrent();
        Current.RenderToPosAndSize(Position, Size);
    }

    public void Update(double dt)
    {
        SinceLastFiller += dt;
        if(Current != null)
            Current.Update(dt);
    }

    void UpdateCurrent()
    {
        if (Current == null)
            Current = Default;
    }
}