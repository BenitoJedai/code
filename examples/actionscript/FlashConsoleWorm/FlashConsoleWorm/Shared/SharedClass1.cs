using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;


namespace FlashConsoleWorm.Shared
{

    public partial class SharedClass1
    {
        // members defined over here can be used on client and on server
        // x


        // A pattern like this gets special treatment:
        // void User...(int user, ...)


        /// <summary>
        /// this interface is to be used in a generator
        /// </summary>
        public partial interface IMessages
        {
      
            // this will generate lots of overkill boilerplate code :)

           
            void ServerPlayerHello(int user, string name);
            void ServerPlayerJoined(int user, string name);
            void ServerPlayerLeft(int user, string name);

            void PlayerAdvertise(string name);
            void UserPlayerAdvertise(int user, string name);


            void MouseMove(int x, int y, int color);
            void UserMouseMove(int user, int x, int y, int color);


            void MouseOut(int color);
            void UserMouseOut(int user, int color);

			void VectorChanged(int x, int y);
			void UserVectorChanged(int user, int x, int y);

			void TeleportTo(int x, int y);
			void UserTeleportTo(int user, int x, int y);

			void EatApple(int x, int y);
			void UserEatApple(int user, int x, int y);

			void EatThisWormBegin(int food);
			void UserEatThisWormBegin(int user, int food);

			void EatThisWormEnd(int food);
			void UserEatThisWormEnd(int user, int food);

			void LevelHasEnded();
			void UserLevelHasEnded(int user);

            void ServerSendMap();

            void SendMap(int[] buttons);
            void UserSendMap(int user, int[] buttons);

            void SendMapLater();
            void UserSendMapLater(int user);

            void SetFlag(int button, int value);
            void UserSetFlag(int user, int button, int value);

            void Reveal(int button);
            void UserReveal(int user, int button);

            void AddScore(int score);
            void AwardAchievementFirstMinefieldComplete();

            void SendPassword(string password);
            void ServerPasswordStatus(int status);


            void LockGame();
            void UnlockGame();

        }
    }
}
