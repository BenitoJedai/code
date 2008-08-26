using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using System.IO;


namespace FlashTreasureHunt.Shared
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



			void ServerPlayerHello(int user, string name, int user_with_map, int[] handshake);
            void ServerPlayerJoined(int user, string name);
            void ServerPlayerLeft(int user, string name);

            void PlayerAdvertise(string name, int[] vector);
			void UserPlayerAdvertise(int user, string name, int[] vector);

			void ServerSendMap();

			void SendMap(int[] bytestream);
			void UserSendMap(int user, int[] bytestream);


			void TakeAmmo(int index);
			void UserTakeAmmo(int user, int index);

			void TakeGold(int index);
			void UserTakeGold(int user, int index);

			void AddDamageToCoPlayer(int target, double damage);
			void UserAddDamageToCoPlayer(int user, int target, double damage);


			void WalkTo(int[] bytestream);
			void UserWalkTo(int user, int[] bytestream);

			void LookAt(double arc);
			void UserLookAt(int user, double arc);

			void FireWeapon();
			void UserFireWeapon(int user);
		}
    }
}
