using VitPro;
using VitPro.Engine;
using System;
using System.Collections.Generic;

class Face : IRenderable, IUpdateable
{
    Player player;
    AnimatedTexture Current;
    AnimatedTexture Default;

    List<AnimatedTexture> Fillers = new List<AnimatedTexture>();
    double SinceLastFiller = 0;
    double TimeToFiller = 4;

    Vec2 Size = new Vec2(10, 10);
    Vec2 Position
    {
        get
        {
            return new Vec2(player.Position.X, player.Position.Y + player.Size.Y - Size.Y);
        }
    }

    Color Back;
    Color Front;

    public Face(Player player)
    {
        this.player = player;
        DefaultLoad.Load(this);
        Back = new Color(0.1, 0.1, 0.1);
        Front = Color.Green;
    }

    public Face AddFiller(AnimatedTexture filler)
    {
        Fillers.Add(filler);
        return this;
    }

    public Face SetDefault(AnimatedTexture t)
    {
        Default = t;
        return this;
    }

    public Face SetBackColor(Color c)
    {
        Back = c;
        return this;
    }

    public Face SetFrontColor(Color c)
    {
        Front = c;
        return this;
    }

    public void Render()
    {
        UpdateCurrent();
        Draw.Rect(Position - Size, Position + Size, Back);
        Draw.Save();
        Draw.Color(Front);
        Current.RenderToPosAndSize(Position, Size);
        Draw.Load();
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
        {
            Current = Default;
        }
        if(Current != Default && Current.HasLooped)
        {
            Current = Default;
            return;
        }
        if (SinceLastFiller > TimeToFiller)
        {
            Current = Fillers[Program.Random.Next(Fillers.Count)];
            SinceLastFiller = 0;
        }
    }
}