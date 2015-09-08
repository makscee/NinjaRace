using VitPro;
using VitPro.Engine;
using System;

class Dead : PlayerState
{
    AnimatedTexture tex;
    double DeathTime = 1;
    public Dead(Player player) : base(player)
    {
        this.player = player;
        tex = new AnimatedTexture();
        for(int i = 1; i <= 7; i++)
            tex.Add(new Texture("./Data/img/player/death/death" + i + ".png"), 0.04);
        tex.Loopable = false;
    }
    public override AnimatedTexture GetTexture()
    {
        return tex;
    }
    public override void Render()
    {
        Vec2 pos1 = player.Dir == 1 ? player.Position - player.Size : player.Position - Vec2.CompMult(player.Size, new Vec2(3, 1));
        Vec2 pos2 = player.Dir == -1 ? player.Position + player.Size : player.Position + Vec2.CompMult(player.Size, new Vec2(3, 1));
        RenderState.Push();
        RenderState.Color = player.Color;
        RenderState.Translate(pos1);
        RenderState.Scale(pos2 - pos1);
        if (player.Dir == -1)
            RenderState.SetOrts(-Vec2.OrtX, Vec2.OrtY, new Vec2(1, 0));
        GetTexture().Render();
        RenderState.Pop();
    }
    public override void Update(double dt)
    {
        tex.Update(dt);
        player.Velocity -= Vec2.Clamp(new Vec2(0, player.Velocity.Y + player.Gravity), player.GAcc * dt);
        bool touch = false;
        if (GetTime() > DeathTime)
        {
            player.Respawn();
        }
        foreach (var a in player.collisions.Values)
            foreach (var b in a)
                if (b.GetType() == typeof(Spikes))
                    return;
                else touch = true;
        if (touch)
            player.Velocity = Vec2.Zero;
    }

    public override void Jump()
    {
    }

    public override void Die(Vec2 position) { }
}