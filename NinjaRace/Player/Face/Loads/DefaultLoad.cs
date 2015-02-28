using VitPro;
using VitPro.Engine;
using System;

class DefaultLoad
{
    public static void Load(Face f)
    {
        f.SetDefault(new AnimatedTexture()
            .Add(new Texture("./Data/img/faces/default/smile1.png"), 3)
            .Add(new Texture("./Data/img/faces/default/smile2.png"), 0.1));
    }
}