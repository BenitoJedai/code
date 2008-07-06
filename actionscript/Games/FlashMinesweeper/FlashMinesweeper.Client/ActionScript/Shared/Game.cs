using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
#if !NoAttributes
using ScriptCoreLib;
#endif
namespace FlashMinesweeper.ActionScript.Shared
{

#if !NoAttributes
    [Script]
#endif
    public class Game : Generic.ServerGameBase<SharedClass1.IEvents, SharedClass1.IMessages, Player>
    {

        public override void UserJoined(Player player)
        {
            Console.WriteLine("UserJoined " + player.Username);


            var x = default(Player);

            foreach (var v in Users)
            {
                if (v.UserId != player.UserId)
                {
                    x = v;
                    break;
                }
            }

            player.FromPlayer.AddScore += e => player.AddScore("score", e.score);


            player.ToPlayer.ServerPlayerHello(player.UserId, player.Username);

            player.ToOthers.ServerPlayerJoined(
               player.UserId, player.Username
            );

            if (x != null)
            {
                x.ToPlayer.ServerSendMap();
            }

        }

        public override void UserLeft(Player player)
        {

            player.ToOthers.ServerPlayerLeft(player.UserId, player.Username);
        }

        public override void GameStarted()
        {
        }

    }
}
