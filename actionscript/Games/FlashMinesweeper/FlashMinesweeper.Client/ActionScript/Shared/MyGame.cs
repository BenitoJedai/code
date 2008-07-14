using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Nonoba.Generic;
namespace FlashMinesweeper.ActionScript.Shared
{

    [Script]
    public class MyGame : ServerGameBase<SharedClass1.IEvents, SharedClass1.IMessages, MyPlayer>
    {


        public override void UserJoined(MyPlayer player)
        {
            Console.WriteLine("UserJoined " + player.Username);


            var x = AnyOtherUser(player);

            player.FromPlayer.AddScore += e => player.AddScore("score", e.score);
            player.FromPlayer.AwardAchievementFirstMinefieldComplete +=
                e => player.AwardAchievement("firstminefielddone");

            player.ToPlayer.ServerPlayerHello(player.UserId, player.Username);

            player.ToOthers.ServerPlayerJoined(
               player.UserId, player.Username
            );

            if (x != null)
            {
                x.ToPlayer.ServerSendMap();
            }

        }

        public override void UserLeft(MyPlayer player)
        {

            player.ToOthers.ServerPlayerLeft(player.UserId, player.Username);
        }

        public override void GameStarted()
        {
        }

    }
}
