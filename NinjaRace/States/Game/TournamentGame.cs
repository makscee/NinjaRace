using VitPro;
using VitPro.Engine;
using System;

class TournamentGame : Game
{
    public TournamentGame(string level)
    {
        Program.Statistics = new Statistics();
        World = new World(level, Program.Tournament.Current.Game.Player1, Program.Tournament.Current.Game.Player2);
        Program.Manager.PushState(this);
        Program.Manager.PushState(new PreGame(World));
    }

    public override void Finish(Player player)
    {
        bool first = Program.WhichPlayer(player) == 0;
        TimerContainer.Clear();
        new TournamentShowdown(World.Level.Name.Trim() + "_S", first);
        Close();
    }
}