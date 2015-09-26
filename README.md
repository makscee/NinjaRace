# NinjaRace

This game is a platformer with tile-based level construction, that is ment to be played only by 2 players (may be more than 2 in future) on the same machine, using 1 keyboard or gamepads (TBD).
Gameplay
--------
  Gameplay consists of 2 stages:
  0. At the beginning players are spawned on separate parts of one level. Splitscreen allows each player to see part of world around them. The goal is to get to a sertain point of level (usually the center). The player who gets there first concidered the winner and gets benefits in stage #2. A player can die or be killed and will be spawned at the beginning of the level.
  0. After first stage, <b>The Showdown</b> begins. Players are spawned in random points of new level. The goal is to kill the opponent player as many times as amout of lives he has. Winner of stage #1 gets 3 lives, loser gets 2. Amout of lives can be seen at the top of the screen.
  
Movements
---------
 Players possible movements are:
 * Running left or right
 * Jumping
 * Wallgrabbing and jumping of the walls
 * Dashing (more detailed below)
  
Swords
------
  Each player has a sword that can be used via specific key. The sword hit is delayed for 0.3 seconds in order to make it a little bit difficult to hit other player.
  
Bonuses
-------
  Each stage (depended on level structure) contains bonus tiles which if touched give player a random bonus. Tile disappears and will be spawned again after some time.
  Here is list of bonuses that are in the game so far:
  * <b>SpeedUp</b> - grant a player instant speed increment for 5 seconds (can't be used via key press as other bonuses)
  * <b>SlowDown</b> - if used, lowers speed of the opponent player for 5 seconds
  * <b>Freeze</b> - if used, frezzes the opponent for 2 seconds (he can't move or do anything at all for this time)
  * <b>JumpBlock</b> - if used, blocks jump ability of the opponent player for 3 seconds (it works as if he couldn't press the jump buttons, that means <b>JumpTiles</b> will still work)
  * <b>Missle</b> - if used, launches a missle from position of player that flies towards the position of opponent player. Upon hit kills the opponent. In the center appears a window with missle flying and the distance to the opponent from missle position. (missle can be dodged if player is <b>dashing</b> by the moment of hit. About <b>dash</b> see below)
  
Dash
----
  Each player can use dash. It can be used in 3 directions: Left, Right, Down. Left and Right are used by pressing left or right move key 2 times quickly. Down dash can be used by pressing down key while in air. Down dash (unlike 2 others) can kill the opponent player on collision. Dash grants immunity to the player, therefore <b>missle</b> can be dodged if hits when player is in state of dash.
  
Tiles
-----
  Levels are build via tiles. 
  Here is list of tiles that are currently in the game:
  * <b>Ground</b> - solid tile that can be walked on
  * <b>CrackedTile</b> - solid tile that can be walked on, disappears after 0.5 seconds
  * <b>BonusTile</b> - upon hit grant player a random bonus
  * <b>StartTile</b> - invisible tile that marks player spawn point
  * <b>FinishTile</b> - used in stage #1, upon hit make a player winner and changes game state to stage #2
  * <b>JumpTile</b> - solid tile, if steped on by a player launches him in air with x2 force of usual jump
  * <b>Saw</b> - moving tile, if touched kills the player
  * <b>Spikes</b> - solid tile that upon touch kills the player
    
Level Editor
------------
TBD
