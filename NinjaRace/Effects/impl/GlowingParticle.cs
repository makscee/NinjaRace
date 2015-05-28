using VitPro;
using VitPro.Engine;
using System;

class GlowingParticle : Effect
{
    Texture tex;
    public Vec2 Size = new Vec2(30, 30), Vel = new Vec2(1, 0);
    Shader s = new Shader(NinjaRace.Shaders.GlowingParticle);
    public double Speed = 200;
    public GlowingParticle(Vec2 pos, Vec2 size, double duration, Color color)
        : base(pos, duration)
    {
        Size = size;
        tex = new Texture(50, 50);
        s.SetVec2("size", new Vec2(tex.Width, tex.Height));
        s.SetTexture("texture", tex);
        s.SetVec4("color", color.R, color.G, color.B, 1);
        tex.ApplyShader(s);
    }

    public GlowingParticle(Vec2 pos, Vec2 size, Color color) : this(pos, size, -1, color) { }

    public GlowingParticle SetSpeed(double speed)
    {
        Speed = speed;
        return this;
    }

    public GlowingParticle SetColor(Color color)
    {
        tex.Dispose();
        tex = new Texture(50, 50);
        s.SetVec4("color", color.R, color.G, color.B, 1);
        tex.ApplyShader(s);
        return this;
    }

    public GlowingParticle SetPosition(Vec2 pos)
    {
        Position = pos;
        return this;
    }

    public GlowingParticle SetVelocity(Vec2 vel)
    {
        vel = vel.Unit;
        Vel = vel;
        return this;
    }

    public override void Render()
    {
        tex.RenderToPosAndSize(Position, Size);
    }

    public override void Update(double dt)
    {
        Position += Vel * Speed * dt;
    }
}