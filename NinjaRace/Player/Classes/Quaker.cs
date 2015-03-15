using VitPro;
using VitPro.Engine;
using System;

class Quaker : Player
{
    public Quaker()
        : base()
    {
        States = new States(this, new RocketJump(this));
        Face.SetFrontColor(Color.Orange);
    }
}