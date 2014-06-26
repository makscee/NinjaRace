﻿using VitPro;
using VitPro.Engine;
using System;

class Game : State
{
    World world = new World();

    public override void Render()
    {
        new Camera(240).Apply();
        Draw.Clear(Color.Black);
        world.Render();
    }
    public override void Update(double dt)
    {
        world.Update(dt);
    }
}