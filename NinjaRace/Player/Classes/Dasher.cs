using System;
using VitPro;
using VitPro.Engine;

class Dasher : Player
{
    public Dasher() : base()
    {
        States = new States(this, new Dash(this));
    }
}