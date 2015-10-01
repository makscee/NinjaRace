using VitPro;
using VitPro.Engine;
using System;
using System.Collections.Generic;

class World : IUpdateable
{
    public Group<Effect> EffectsTop = new Group<Effect>();
    public Group<Effect> EffectsMid = new Group<Effect>();
    public Group<Effect> EffectsBot = new Group<Effect>();
    public Group<Effect> EffectsScreen = new Group<Effect>();
    public Player Player1, Player2;
    public Dictionary<Player, Group<Player>> Copies = new Dictionary<Player, Group<Player>>();
    public Level Level;
    public MissleEffect CurrentMissle = null;
    Label MissleDistance = new Label("123", 40);
    View cam1 = new View(240), cam2 = new View(240), cam = new View(360), screenCam = new View(120);
    Vec2 CamOffset = new Vec2(0, 120);
    public double Time = 0;

    private void Init()
    {
        List<StartTile> starts = Level.Tiles.GetStartTiles();
        Vec2 pos1 = starts[0].Position, pos2 = starts[starts.Count - 1].Position;

        Player1 = new Player(pos1.X < pos2.X ? pos1 : pos2,
            Color.White).SetControls(Program.Settings.GetPlayer1Controller());
        Player2 = new Player(pos1.X > pos2.X ? pos1 : pos2,
            new Color(0.5, 0.7, 0.7)).SetControls(Program.Settings.GetPlayer2Controller());
        Copies.Add(Player1, new Group<Player>());
        Copies.Add(Player2, new Group<Player>());
        Player1.CalculateCollisions();
        Player2.CalculateCollisions();
        Player2.Dir = -1;
        cam1.Position = Player1.Position;
        cam2.Position = Player2.Position;
        MissleDistance.BackgroundColor = Color.White;
        MissleDistance.Anchor = new Vec2(0.7, 0.5);
    }

    public World(string level)
    {
        Program.World = this;
        this.Level = DBUtils.GetLevel(level);
        Init();
    }

    public bool RenderScreenEffects = true;

    public void Render(Player player = null)
    {
        Level.Tiles.RenderBackground();
        EffectsBot.Render();
        if (player != null)
            Level.RenderArea(player.Position, new Vec2(360, 160));
        else Level.Render();
        EffectsMid.Render();
        Player2.Render();
        Player1.Render();
        Copies[Player1].Render();
        Copies[Player2].Render();
        EffectsTop.Render();
    }

    public void RenderSingle()
    {
        cam.FOV = Math.Max(360, Math.Max(
            Math.Abs(Player1.Position.X - Player2.Position.X) + 15, 
            Math.Abs(Player1.Position.Y - Player2.Position.Y) + 60));
        cam.Position = (Player2.Position - Player1.Position) / 2 + Player1.Position;
        cam.Apply();
        Render();
		RenderState.Push();
        screenCam.Apply();
        if(RenderScreenEffects)
            EffectsScreen.Render();
		RenderState.Pop();
    }

    public void RenderSplit()
    {
        RenderState.Push();
        RenderState.BeginArea(new Vec2i(0, RenderState.Height / 2), 
            new Vec2i(RenderState.Width, RenderState.Height / 2));
        Draw.Clear(Color.Black);
        RenderState.Translate(Vec2.OrtY * 0.5);
        View camt = new View(cam1.FOV);
        camt.Position = cam1.Position + CamOffset;
        camt.Apply();
        RenderState.Translate(Vec2.OrtY * cam1.FOV / 2);
        DrawBackground(Player1);
        Render(Player1);
        RenderState.EndArea();
        RenderState.Pop();

        RenderState.Push();
        RenderState.BeginArea(new Vec2i(0, 0),
            new Vec2i(RenderState.Width, RenderState.Height / 2));
        Draw.Clear(Color.Black);
        RenderState.Translate(Vec2.OrtY * 0.5);
        camt = new View(cam1.FOV);
        camt.Position = cam2.Position + CamOffset;
        camt.Apply();
        RenderState.Translate(Vec2.OrtY * cam2.FOV / 2);
        DrawBackground(Player2);
        Render(Player2);
        RenderState.EndArea();
        RenderState.Pop();

        
        if (EffectsTop.Contains(CurrentMissle))
        {
            MissleEffectRender();
        }

		RenderState.Push();
        screenCam.Apply();
        if(RenderScreenEffects)
            EffectsScreen.Render();
		RenderState.Pop();
    }

    private void MissleEffectRender()
    {
        RenderState.Push();
        Vec2i WindowSize = new Vec2i(RenderState.Height / 6, RenderState.Height / 6);
        RenderState.BeginArea(new Vec2i(RenderState.Width / 2, RenderState.Height / 2) - WindowSize / 2,
            WindowSize);
        View m = new View(120);
        m.Position = CurrentMissle.MainParticle.Position;
        m.Apply();
        Level.Tiles.RenderBackground();
        EffectsBot.Render();
        Level.RenderArea(CurrentMissle.MainParticle.Position, new Vec2(150, 150));
        EffectsMid.Render();
        Player2.Render();
        Player1.Render();
        EffectsTop.Render();
        RenderState.EndArea();
        RenderState.Pop();
        RenderState.Push();
        RenderState.View2d(0, 0, RenderState.Width, RenderState.Height);
        RenderState.Translate(RenderState.Width / 1.6, RenderState.Height / 2);
        double t = (CurrentMissle.MainParticle.Position - CurrentMissle.Player.Position).Length;
        RenderState.Scale(RenderState.Height / 15);
        if (t < 1500)
        {
            RenderState.Color = new Color(1, t / 750, t / 1500);
            RenderState.Scale(2 - t / 1500);
        }
        RenderState.Origin(0.5, 0.5);
        Program.font.Render(Math.Floor(t).ToString());
        RenderState.Pop();
    }

    private void UpdateForPlayer(double dt, Player player)
    {
        player.CalculateCollisions();
        player.Update(dt);
        View cam = (player == Player1) ? cam1 : cam2;
        cam.Position += (player.Position - cam.Position) * dt * 10;
        player.Position += player.Velocity * dt;
    }

    private void UpdateCopy(double dt, Player player)
    {
        player.CalculateCollisions();
        player.Update(dt);
        player.Position += player.Velocity * dt;
    }

    public double SlowTime = 1;
    public void Update(double dt)
    {
        if (SlowTime < 0.5)
            SlowTime += dt / 2;
        else SlowTime = 1;
        dt = SlowTime * dt;
        TimerContainer.Update(dt);
        Time += dt;
        UpdateForPlayer(dt, Player1);
        UpdateForPlayer(dt, Player2);
        foreach (var l in Copies)
            foreach(var p in l.Value)
                UpdateCopy(dt, p);
        Level.Update(dt);
        EffectsTop.Update(dt);
        EffectsTop.Refresh();
        EffectsMid.Update(dt);
        EffectsMid.Refresh();
        EffectsBot.Update(dt);
        EffectsBot.Refresh();
        EffectsScreen.Update(dt);
        EffectsScreen.Refresh();
        Copies[Player1].Refresh();
        Copies[Player2].Refresh();
    }
    public void KeyDown(Key key)
    {
        Player1.Controller.KeyDown(key);
        Player2.Controller.KeyDown(key);
        foreach (var l in Copies)
            foreach (var p in l.Value)
                p.Controller.KeyDown(key);
        if (key == Key.R)
            new ShadowCopy().Get(Player1);
    }
    public void KeyUp(Key key)
    {
        Player1.Controller.KeyUp(key);
        Player2.Controller.KeyUp(key);
        foreach (var l in Copies)
            foreach (var p in l.Value)
                p.Controller.KeyUp(key);
    }

    private void DrawBackground(Player player)
    {
        //if (background == null)
        //    return;
        //RenderState.Push();
        //new Camera(360).Apply();
        //Vec2 offset = new Vec2(player.Position.X / 3, 0);
        //offset = new Vec2(offset.X - App.Width * Math.Floor(offset.X / App.Width), 0);
        //RenderState.Translate(-offset);
        //RenderState.Scale(App.Width / 2, App.Height / 3);
        //RenderState.Scale(2);
        //RenderState.Origin(0.5, 0.5);
        //background.Render();
        //RenderState.Translate(new Vec2(1, 0));
        //background.Render();
        //RenderState.Pop();
    }
}