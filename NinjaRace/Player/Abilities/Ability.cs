using VitPro;
using VitPro.Engine;
using System;

class Ability : IUpdateable, IRenderable
{
    protected Player player;
    protected PlayerState state;

    public Ability(Player player)
    {
        this.player = player;
    }

    public virtual void Use() { }

    public virtual void Render() { }
    
    public virtual void Update(double dt) { }
}