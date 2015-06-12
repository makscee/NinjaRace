using VitPro;
using VitPro.Engine;
using System;

class World : IRenderable, IUpdateable
{
    public Group<Effect> EffectsTop = new Group<Effect>();
    public Group<Effect> EffectsMid = new Group<Effect>();
    public Group<Effect> EffectsBot = new Group<Effect>();
    public Group<Effect> EffectsScreen = new Group<Effect>();
    public Player player1, player2;
    public Level level;
    Camera cam1 = new Camera(540), cam2 = new Camera(540), cam = new Camera(360), screenCam = new Camera(120);
    Vec2 camOffset = new Vec2(0, 120);
    public double Time = 0;
    public Texture background;

    private void Init()
    {
        player1 = new Player(this.level.tiles.GetStartTiles().Item1.Position).SetControls(new ControllerPlayer1());
        player2 = new Player(this.level.tiles.GetStartTiles().Item2.Position).SetControls(new ControllerPlayer2());
    }

    public World()
    {
        Program.World = this;
        level = DBUtils.GetLevel("default");
        Init();
    }

    public World(string level)
    {
        Program.World = this;
        this.level = DBUtils.GetLevel(level);
        Init();
        cam.Position = new Vec2(cam.FOV / 2, cam.FOV / 3);
    }

    public void Render()
    {
        EffectsBot.Render();
        level.Render();
        EffectsMid.Render();
        player2.Render();
        player1.Render();
        EffectsTop.Render();
    }

    public void RenderSingle()
    {
        cam.FOV = Math.Max(360, Math.Abs(player1.Position.X - player2.Position.X) + 15);
        cam.Position = (player2.Position - player1.Position) / 2 + player1.Position;
        cam.Apply();
        Render();
		RenderState.Push();
        screenCam.Apply();
        EffectsScreen.Render();
		RenderState.Pop();
    }

    public void RenderSplit()
    {
		Texture tex = new Texture(RenderState.Width, RenderState.Height);
		RenderState.BeginTexture(tex);
        Draw.Clear(Color.Black);
        Camera camt = new Camera(cam1.FOV);
        camt.Position = cam1.Position + camOffset;
        camt.Apply();
        DrawBackground(player1);
        Render();
		RenderState.EndTexture();
        tex.RemoveAlpha();

		RenderState.Push();
		RenderState.Scale(2);
		RenderState.Origin(0.5, 0.5);
		RenderState.Translate(0, 0.5);
        tex.Render();
		RenderState.Pop();

		tex = new Texture(RenderState.Width, RenderState.Height);
		RenderState.BeginTexture(tex);
        Draw.Clear(Color.Black);
        camt.Position = cam2.Position - camOffset;
        camt.Apply();
        DrawBackground(player2);
        Render();
		RenderState.EndTexture();
        tex.RemoveAlpha();
		RenderState.Push();
		RenderState.Scale(2);
		RenderState.Origin(0.5, 0.5);
		RenderState.Translate(0, -0.5);
        tex.Render();
		RenderState.Pop();
		RenderState.Push();
        screenCam.Apply();
        EffectsScreen.Render();
		RenderState.Pop();
    }

    private void UpdateForPlayer(double dt, Player player)
    {
        if (!(player.States.current is Win))
            Time += dt;
        player.CalculateCollisions();
        player.Update(dt);
        Camera cam = (player == player1) ? cam1 : cam2;
        cam.Position += (player.Position - cam.Position) * dt * 10;
        player.Position += player.Velocity * dt;

        foreach (var a in player.collisions[Side.Left])
            a.Effect(player, Side.Left);
        foreach (var a in player.collisions[Side.Down])
            a.Effect(player, Side.Down);
        foreach (var a in player.collisions[Side.Right])
            a.Effect(player, Side.Right);
        foreach (var a in player.collisions[Side.Up])
            a.Effect(player, Side.Up);
        level.Update(dt);
    }

    public void Update(double dt)
    {
        UpdateForPlayer(dt, player1);
        UpdateForPlayer(dt, player2);
        EffectsTop.Update(dt);
        EffectsTop.Refresh();
        EffectsMid.Update(dt);
        EffectsMid.Refresh();
        EffectsBot.Update(dt);
        EffectsBot.Refresh();
        EffectsScreen.Update(dt);
        EffectsScreen.Refresh();
    }
    public void KeyDown(Key key)
    {
        player1.Controller.KeyDown(key);
        player2.Controller.KeyDown(key);
    }
    public void KeyUp(Key key)
    {
        player1.Controller.KeyUp(key);
        player2.Controller.KeyUp(key);
    }

    private void DrawBackground(Player player)
    {
        if (background == null)
            return;
		RenderState.Push();
        new Camera(360).Apply();
        Vec2 offset = new Vec2(player.Position.X / 3, 0);
        offset = new Vec2(offset.X - App.Width * Math.Floor(offset.X / App.Width), 0);
		RenderState.Translate(-offset);
		RenderState.Scale(App.Width / 2, App.Height / 3);
		RenderState.Scale(2);
		RenderState.Origin(0.5, 0.5);
        background.Render();
		RenderState.Translate(new Vec2(1, 0));
        background.Render();
		RenderState.Pop();
    }
}