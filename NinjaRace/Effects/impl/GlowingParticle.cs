using VitPro;
using VitPro.Engine;
using System;


class GlowingParticle : Effect
{
    Texture tex;
    public Vec2 Size = new Vec2(30, 30), Vel = Vec2.Zero, NeedVel = Vec2.Zero;
    Shader s = new Shader(NinjaRace.Shaders.GlowingParticle);
    public double Speed = 200, Acc = 100;
    public GlowingParticle(Vec2 pos, Vec2 size, Color color)
        : base(pos)
    {
        Size = size;
        tex = new Texture(50, 50);
        s.SetVec2("size", new Vec2(tex.Width, tex.Height));
        s.SetTexture("texture", tex);
        s.SetVec4("color", color.R, color.G, color.B, 1);
        tex.ApplyShader(s);
    }

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

    public GlowingParticle SetNeedVelocity(Vec2 vel)
    {
        NeedVel = vel.Unit;
        return this;
    }

    public GlowingParticle SetVelocity(Vec2 vel)
    {
        Vel = vel;
        return this;
    }

    public GlowingParticle SetAcc(double acc)
    {
        Acc = acc;
        return this;
    }

    public override void Render()
    {
        
        tex.RenderToPosAndSize(Position, Size);
    }

    public override void Update(double dt)
    {
        Vel -= Vec2.Clamp(Vel - NeedVel * Speed, Acc);
        Position += Vel * dt;
    }

    public override void Dispose()
    {
        base.Dispose();
        tex.Dispose();
    }
}