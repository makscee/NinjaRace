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
        if (player.collisions[Side.Left].Count == 0 &&
           player.collisions[Side.Right].Count == 0 &&
           player.collisions[Side.Down].Count == 0)
        {

            current = flying;
            return;
        }
        if (player.collisions[Side.Down].Count != 0)
        {
            current = walking;
            return;
        }
        if (player.collisions[Side.Down].Count == 0)
        {
            current = wallgrab;
            return;
        }
    }

    public void Set(PlayerState state)
    {
        current = state;
    }

    public void SetFlying()
    {
        current = flying;
    }

    public void SetDead()
    {
        current = dead;
    }

    public void Reset()
    {
        current = null;
        StateChangeCheck();
    }

    public void Render()
    {current.Render();
    }

    public void Update(double dt)
    {
        StateChangeCheck();
        if (player.Controller.NeedJump())
            current.Jump();
        if (player.Controller.NeedAbility())
            current.AbilityUse(ability);
        current.Update(dt);
    }
            
}