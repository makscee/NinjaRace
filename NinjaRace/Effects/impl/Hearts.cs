using System;
using VitPro;
using VitPro.Engine;

 class Hearts : Effect
 {
     Texture tex = new Texture("./Data/img/heart.png");

     Vec2 size = new Vec2(3, 3);
     double dist = 7;

     Player player;

     bool left;

     public Hearts(Player player) 
         : base(player == Program.World.player1 ? new Vec2(-70, 50) : new Vec2(70, 50)) 
     {
         left = player == Program.World.player1;
         this.player = player;
     }

     public override void Render()
     {
         for (int i = 0; i < player.Lives; i++)
         {
             Draw.Texture(tex, Position - size + Vec2.OrtX * dist * i * (left ? 1 : -1),
                 Position + size + Vec2.OrtX * dist * i * (left ? 1 : -1));
         }
     }
 }