using VitPro;
using VitPro.Engine;
using System;

class Dash : PlayerState
{
    Vec2 dir = Vec2.Zero;
    static AnimatedTexture down, side;
    double speed, t = 0, mt = 0.07, accFactor = 3;

    public Dash(Player player, Vec2 dir)
        : base(player)
    {
        this.dir = dir;
        speed = player.Speed * accFactor;
        down = new AnimatedTexture();
        for (int i = 1; i <= 11; i++)
            down.Add(new Texture("./Data/img/player/downdash/dash" + i + ".png"), 0.04);
        side = new AnimatedTexture();
        for (int i = 1; i < 7; i++)
        {
            side.Add(new Texture("./Data/img/player/run/player_run" + i.ToString() + ".png"), 0.03);
        }
    }

    public override AnimatedTexture GetTexture()
    {
        if (dir.Y != 0)
            return down;
        else return side;
    }
    public override void Render()
    {
        Vec2 pos1 = player.Dir == 1 ? player.Position - player.Size : player.Position -
            Vec2.CompMult(player.Size, new Vec2(1, dir.Y != 0 ? 1.33 : 1));
        Vec2 pos2 = player.Dir == -1 ? player.Position + player.Size : player.Position +
            Vec2.CompMult(player.Size, new Vec2(1, dir.Y != 0 ? 1.33 : 1));
        RenderState.Push();
        RenderState.Color = Color.Mix(Color.Yellow, player.Color);
        RenderState.Translate(pos1);
        RenderState.Scale(pos2 - pos1);
        if (player.Dir == -1)
            RenderState.SetOrts(-Vec2.OrtX, Vec2.OrtY, new Vec2(1, 0));
        GetTexture().Render();
        RenderState.Pop();
    }
    public override void Update(double dt)
    {
        GetTexture().Update(dt);
        if (dir.Y == 0)
            t += dt;
        player.Velocity = dir * speed;
        if (dir.X != 0 && (player.collisions[Side.Left].Count > 0 || player.collisions[Side.Right].Count > 0) ||
            dir.Y != 0 && player.collisions[Side.Down].Count > 0 || t > mt)
        {
            player.States.Reset();
            player.Velocity = Vec2.Zero;
            t = 0;
        }
        if (!Program.IsCopy(player))
        {
            foreach (var a in player.GetAllOpponents())
            {
                if (!a.States.IsDead && dir.Y != 0 && player.Box.Collide(a.Box) != Side.None)
                {
                    if(!Program.IsCopy(a))
                        Program.Statistics.Kills[Program.WhichPlayer(player)]++;
                    a.States.current.Die(player.Position);
                }
            }
        }
    }

    public override void Jump() { }

    public override void Die(Vec2 position) 
    {
        if (Program.IsCopy(player))
            base.Die(position);
    }
}