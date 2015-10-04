using VitPro;
using VitPro.Engine;
using System;

class TournamentShowdown : Showdown
{
    public TournamentShowdown(string level, bool first)
    {
        World = new World(level, Program.Tournament.Current.Game.Player1, Program.Tournament.Current.Game.Player2);

        if (first)
            World.Player2.Lives = 2;
        else World.Player1.Lives = 2;
        World.EffectsScreen.Add(new Hearts(World.Player1));
        World.EffectsScreen.Add(new Hearts(World.Player2));

        World.Player1.Respawn = World.Player1.ChangeSpawn + World.Player1.Respawn;
        World.Player2.Respawn = World.Player2.ChangeSpawn + World.Player2.Respawn;

        Program.Manager.PushState(this);
        Program.Manager.PushState(new PreShowdown(World));
    }

    protected override void Finish()
    {
        Player p = (World.Player1.Lives < 1 ? World.Player2 : World.Player1);
        bool first = Program.WhichPlayer(p) == 0;
        Program.Tournament.Current.Done(first ? Tournament.Result.P1 : Tournament.Result.P2);
        Close();
        TimerContainer.Clear();
    }
}