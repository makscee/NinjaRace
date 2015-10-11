using VitPro;
using VitPro.Engine;
using System;
using System.Collections.Generic;


class Tournament : VitPro.Engine.UI.State
{
    string Level;
    public enum Result
    {
        P1, P2, Undefiend
    }
    public GameTree.GameNode Current = null;
    public class GameTree
    {
        public class Game
        {
            public Player Player1, Player2;
            public Result Result = Result.Undefiend;
        }
        public class GameNode : VitPro.Engine.UI.Element
        {
            public Game Game;
            public GameNode Left, Right;
            GameNode NodeParent;
            public GameNode() 
            {
                BackgroundColor = Color.White;
                Size = new Vec2(60, 30);
                Game = new Game();
            }
            public GameNode AddLeft(GameNode node)
            {
                Left = node;
                node.NodeParent = this;
                return this;
            }

            public GameNode AddRight(GameNode node)
            {
                Right = node;
                node.NodeParent = this;
                return this;
            }

            public GameNode Done(Result result)
            {
                Player Winner = (result == Result.P1) ? Game.Player1 : Game.Player2;
                Game.Result = result;
                if (NodeParent == null)
                    return this;
                if (NodeParent.Left == this)
                {
                    NodeParent.Game.Player1 = Winner;
                }
                else if (NodeParent.Right == this)
                {
                    NodeParent.Game.Player2 = Winner;
                }
                return this;
            }

            AnimatedTexture p1, p2;
            public override void Render()
            {
                if (p1 == null && Game.Player1 != null)
                {
                    p1 = Game.Player1.States.current.GetTexture();
                }
                if (p2 == null && Game.Player2 != null)
                {
                    p2 = Game.Player2.States.current.GetTexture();
                }
                RenderState.Push();
                RenderState.Color = new Color(0.1, 0.1, 0.1);
                Draw.Rect(BottomLeft - new Vec2(15, 15), TopRight + new Vec2(15, 25));
                if (Game.Result != Result.Undefiend)
                    RenderState.Color = Color.Gray;
                if (p1 != null && Game.Result != Result.P1)
                {
                    RenderState.Push();
                    if (Game.Result == Result.Undefiend)
                        RenderState.Color = Game.Player1.Color;
                    RenderState.Translate(-10, 0);
                    Draw.Texture(p1.GetCurrent(), BottomLeft, BottomLeft + new Vec2(Size.X / 2, Size.Y * 1.58));
                    RenderState.Pop();
                }
                if (p2 != null && Game.Result != Result.P2)
                {
                    RenderState.Push();
                    if (Game.Result == Result.Undefiend)
                        RenderState.Color = Game.Player2.Color;
                    RenderState.Translate(10, 0);
                    Draw.Texture(p2.GetCurrent(), BottomRight, BottomLeft + new Vec2(Size.X / 2, Size.Y * 1.58));
                    RenderState.Pop();
                }
                RenderState.Pop();
            }
            public override void Update(double dt)
            {
                base.Update(dt);
                if (Game.Result != Result.Undefiend)
                    return;
                if (p1 != null)
                    p1.Update(dt);
                if (p2 != null)
                    p2.Update(dt);
            }
        }

        public GameNode Head;
        public int Players, Levels;

        public GameTree(int players)
        {
            Players = players;
            Levels = (int)Math.Ceiling(Math.Log(players, 2));
            Head = new GameNode();
            InitTree(Head);
            List<Color> Colors = new List<Color>();
            for (int i = 0; i < players; i++)
            {
                Color c = new Color(Program.Random.NextDouble(), Program.Random.NextDouble(),
                        Program.Random.NextDouble());
                bool good = true;
                foreach (var a in Colors)
                {
                    if (Math.Abs(a.R - c.R) < 0.3 || Math.Abs(a.G - c.G) < 0.3 || Math.Abs(a.B - c.B) < 0.3)
                    {
                        good = false;
                        break;
                    }
                    if (c.R < 0.15 && c.G < 0.15 && c.B < 0.15)
                    {
                        good = false;
                        break;
                    }
                }
                if (!good)
                    i--;
            }
            foreach (var a in GetLevel(Head, Levels))
                a.Game.Player1 = new Player(Vec2.Zero,
                    new Color(Program.Random.NextDouble(), Program.Random.NextDouble(),
                        Program.Random.NextDouble())).SetControls(Program.Settings.GetPlayer1Controller());
            int left = players - (int)Math.Pow(2, Levels - 1);
            foreach (var a in GetLevel(Head, Levels))
            {
                if (left-- == 0)
                    break;
                a.Game.Player2 = new Player(Vec2.Zero,
                    new Color(Program.Random.NextDouble(), Program.Random.NextDouble(),
                        Program.Random.NextDouble())).SetControls(Program.Settings.GetPlayer2Controller());
            }

        }

        public IEnumerable<GameNode> GetLevel(GameNode node, int level, int curLevel = 1)
        {
            if (curLevel == level)
                yield return node;
            else
            {
                foreach (var a in GetLevel(node.Left, level, curLevel + 1))
                    yield return a;
                foreach (var a in GetLevel(node.Right, level, curLevel + 1))
                    yield return a;
            }
        }

        void InitTree(GameNode node, int curLevel = 2)
        {
            if (curLevel > Levels)
                return;
            GameNode left = new GameNode();
            InitTree(left, curLevel + 1);
            GameNode right = new GameNode();
            InitTree(right, curLevel + 1);
            node.AddLeft(left).AddRight(right);
        }

        GameNode NextGame = null;
        void FindNextGame(GameNode current)
        {
            if (NextGame != null)
                return;
            if (current.Game.Result == Result.Undefiend &&
                (current.Left == null || current.Left.Game.Result != Result.Undefiend) &&
                (current.Right == null || current.Right.Game.Result != Result.Undefiend))
            {
                NextGame = current;
                return;
            }
            if (current.Left != null && current.Left.Game.Result == Result.Undefiend)
            {
                FindNextGame(current.Left);
            }
            if (current.Right != null && current.Right.Game.Result == Result.Undefiend)
            {
                FindNextGame(current.Right);
            }
        }
        public GameNode GetNextGame()
        {
            for (int i = Levels; i > 0; i--)
            {
                foreach (var a in GetLevel(Head, i))
                {
                    if (a.Game.Result == Result.Undefiend)
                        return a;
                }
            }
            return null;
        }
    }

    GameTree Tree;

    public Tournament(int players, string level)
    {
        Level = level;
        Program.Tournament = this;
        Tree = new GameTree(players);
        for (int i = Tree.Levels; i > 0; i--)
        {
            int j = 0;
            int LevelSize = (int)Math.Pow(2, i - 1) + 1;
            foreach (var a in Tree.GetLevel(Tree.Head, i))
            {
                a.Anchor = new Vec2((1.0 / LevelSize) * ++j, (1.0 / (Tree.Levels + 1)) * (Tree.Levels - i + 1));
                Frame.Add(a);
            }
        }
    }

    public override void Render()
    {
        Draw.Clear(Color.Black);
        base.Render();
    }

    public override void Update(double dt)
    {
        base.Update(dt);
        if (Current == Tree.Head && Current.Game.Result != Result.Undefiend)
        {
            Close();
            Program.Manager.PushState(new TournamentGameOver(Current.Game.Result == Result.P1 ?
                Current.Game.Player1 : Current.Game.Player2));
            return;
        }
        if (Current == null || Current.Game.Result != Result.Undefiend)
        {
            Current = Tree.GetNextGame();
            while (Current.Game.Player2 == null)
            {
                Current.Done(Result.P1);
                Current = Tree.GetNextGame();
            }
            Current.Game.Player1.SetControls(Program.Settings.GetPlayer1Controller());
            Current.Game.Player2.SetControls(Program.Settings.GetPlayer2Controller());
        }
    }

    public override void KeyDown(Key key)
    {
        base.KeyDown(key);
        if (key == Key.R)
        {
            Current.Done(Result.P1);
        }
        else new TournamentGame(Level);
    }
}