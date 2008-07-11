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
        }
    }
}
