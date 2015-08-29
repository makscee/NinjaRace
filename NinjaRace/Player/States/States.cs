using VitPro;
using VitPro.Engine;
using System;

class States : IRenderable, IUpdateable
{
    Player player;
    public PlayerState current;
    public States(Player player) 
    { 
        this.player = player;
        falling = new Falling(player);
        walking = new Walking(player);
        dead = new Dead(player);
        wallgrab = new WallGrab(player);
        jump = new Jump(player);
        doublejump = new DoubleJump(player);
    }

    PlayerState falling, walking, dead, wallgrab, jump, doublejump;

    public void StateChangeCheck()
    {
        if (current == dead)
            return;
        if(!(current == walking || current == falling || current == wallgrab || current == jump || current == doublejump) && current != null)
            return;
        if (!player.TouchWalls())
        {
            if (current is Falling)
                return;
            current = falling;
            current.Reset();
            return;
        }
        if (player.OnGround())
        {
            if (current == jump && player.Velocity.Y > 0)
                return;
            if (current == walking)
                return;
            current = walking;
            current.Reset();
            return;
        }
        if (!player.OnGround() && !(current == wallgrab))
        {
            current = wallgrab;
            current.Reset();
            return;
        }
    }

    public void Set(PlayerState state)
    {
        if (current == state)
            return;
        current = state;
        current.Reset();
    }

    public void SetFalling(bool canJump = true)
    {
        ((Falling)falling).CanJump = canJump;
        Set(falling);
    }

    public void SetDead()
    {
        Set(dead);
        player.NextDeath.Apply();
        player.NextDeath = () => { };
    }

    public void SetWalking()
    {
        Set(walking);
    }

    public void Jump()
    {
        if (current == walking)
        {
            Set(jump);
            return;
        }
        if(current == falling)
        {
            Set(doublejump);
            return;
        }
        if (current == jump)
        {
            Set(doublejump);
            return;
        }
    }

    public void Reset()
    {
        current = null;
        StateChangeCheck();
        current.Reset();
    }

    public void Render()
    {
        current.Render();
    }

    public void Update(double dt)
    {
        StateChangeCheck();
        if (player.Controller.NeedJump())
            current.Jump();
        current.Update(dt);
        current.AddTime(dt);
    }

    public double GetTime() { return current.GetTime(); }

    public bool IsFlying { get { return current == falling || current == jump || current == doublejump; } }
    public bool IsWalking { get { return current == walking; } }
    public bool IsDead { get { return current == dead; } }
    public bool IsWallgrab { get { return current == wallgrab; } }
}