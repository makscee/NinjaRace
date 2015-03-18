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
    int Dir = 1;
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
        if(Dir == 1)
            Current.RenderToPosAndSize(Position, new Vec2(Size.X, Size.Y));
        if(Dir == -1)
            Current.RenderToPosAndSize(Position, new Vec2(-Size.X, Size.Y));
        Draw.Load();
    }

    public void Update(double dt)
    {
        if(Current == Default)
            SinceLastFiller += dt;
        if(Current != null)
            Current.Update(dt);
        if (player.Velocity.X > 0)
            Dir = 1;
        if (player.Velocity.X < 0)
            Dir = -1;
    }

    void UpdateCurrent()
    {
        if (player.States.current.Animated)
        {
            Current = player.States.current.GetTexture();
            return;
        }
        if (Current == null)
        {
            Current = Default;
        }
        if(Current != Default && Current.HasLooped)
        {
            Current = Default.Reset();
            return;
        }
        if (SinceLastFiller > TimeToFiller)
        {
            Current = Fillers[Program.Random.Next(Fillers.Count)].Reset();
            SinceLastFiller = 0;
        }
    }
}