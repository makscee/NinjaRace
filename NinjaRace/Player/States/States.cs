using VitPro;
using VitPro.Engine;
using System;

class States : IRenderable, IUpdateable
{
    Player player;
    public PlayerState current;
    Ability ability;
    public States(Player player, Ability ability) 
    { 
        this.player = player;
        flying = new Flying(player);
        walking = new Walking(player);
        dead = new Dead(player);
        wallgrab = new WallGrab(player);
        this.ability = ability;
    }

    PlayerState flying, walking, dead, wallgrab;

    public void StateChangeCheck()
    {
        if (current == dead)
            return;
        if(!(current == walking || current == flying || current == wallgrab) && current != null)
            return;
        if (!player.TouchWalls())
        {
            if (current == flying)
                return;
            current = flying;
            current.Reset();
            return;
        }
        if (player.OnGround())
        {
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
        current = state;
        current.Reset();
    }

    public void SetFlying()
    {
        current = flying;
        current.Reset();
    }

    public void SetDead()
    {
        current = dead;
        current.Reset();
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
        if (player.Controller.NeedAbility())
            current.AbilityUse(ability);
        current.Update(dt);
        current.AddTime(dt);
    }
            
}