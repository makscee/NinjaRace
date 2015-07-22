using System;
using VitPro;
using VitPro.Engine;

class Level : IRenderable, IUpdateable
{
    public Tiles Tiles;
    public string Name;

    public Level(Tiles tiles, string name)
    {
        this.Tiles = tiles;
        this.Name = name;
    }

    public void Render() 
    {
        Tiles.Render();
    }
    public void RenderArea (Vec2 pos, Vec2 size)
    {
        Tiles.RenderArea(pos, size);
    }
    public void Update(double dt) 
    {
        Tiles.Update(dt);
    }
}