using System;
using System.Collections.Generic;
using System.Text;

namespace LightsOut.ActionScript.Shared
{
    public partial class SharedClass1
    {
        public partial interface IMessages
        {
            // this will generate lots of overkill boilerplate code :)


            void ServerPlayerHello(int user, string name);
            void ServerPlayerJoined(int user, string name);
            void ServerPlayerLeft(int user, string name);

            void PlayerAdvertise(string name);
            void UserPlayerAdvertise(int user, string name);


            void ServerSendMap();

            void SendMap(int[] data);
            void UserSendMap(int user, int[] data);

            void Click(int x, int y);
            void UserClick(int user, int x, int y);

            void MouseMove(int x, int y, int color);
            void UserMouseMove(int user, int x, int y, int color);


            void AddScore(int score);

            void AwardCompletedThree();
            void AwardCompletedTen();
        }
    }
}
