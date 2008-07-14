using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;

using ScriptCoreLib;
using ScriptCoreLib.Shared.Nonoba.Generic;

namespace LightsOut.ActionScript.Shared
{
    [Script]
    public class MyGame : ServerGameBase<SharedClass1.IEvents, SharedClass1.IMessages, MyPlayer>
    {

        public override void UserJoined(MyPlayer player)
        {
            Console.WriteLine("UserJoined " + player.Username);


       

            player.FromPlayer.AddScore += e => player.AddScore("score", e.score);

            player.FromPlayer.AwardCompletedTen += e => player.AwardAchievement("completedten");
            player.FromPlayer.AwardCompletedThree += e => player.AwardAchievement("completedthree");

            player.ToPlayer.ServerPlayerHello(player.UserId, player.Username);

            player.ToOthers.ServerPlayerJoined(
               player.UserId, player.Username
            );

            var x = AnyOtherUser(player);

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
