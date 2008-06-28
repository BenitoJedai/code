using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;

#if !NoAttributes
using ScriptCoreLib;
#endif

namespace FlashTowerDefense.Shared
{


    public partial class SharedClass1
    {
        // members defined over here can be used on client and on server
        // x

        public string Hello;

        /// <summary>
        /// this interface is to be used in a generator
        /// </summary>
        public partial interface IMessages
        {
            // this will generate lots of overkill boilerplate code :)

            // client -> server  -> others
            void TeleportTo(int x, int y);
            // server -> others
            void UserTeleportTo(int user, int x, int y);

            // client -> server  -> others
            void WalkTo(int x, int y);
            // server -> others
            void UserWalkTo(int user, int x, int y);


            // client -> server
            void CancelServerRandomNumbers();
            void ReadyForServerRandomNumbers();

            

            // client -> server  -> others
            void TakeBox(int box);
            // server -> others
            void UserTakeBox(int user, int box);

            void FiredShotgun();
            void UserFiredShotgun(int user);

            void ServerRandomNumbers(double[] e);
            void ServerMessage(string text);

            void UserEnterMachineGun(int user);
            void UserExitMachineGun(int user);
            void UserStartMachineGun(int user);
            void UserStopMachineGun(int user);

            void EnterMachineGun();
            void ExitMachineGun();
            void StartMachineGun();
            void StopMachineGun();
            // ...

            void Ping();

            void AddDamageFromDirection(int target, int damage, int arc);
            void UserAddDamageFromDirection(int user, int target, int damage, int arc);

            void ShowBulletsFlying(int x, int y, int arc, int weapon);
            void UserShowBulletsFlying(int user, int x, int y, int arc, int weapon);

            // ...

            void ServerPlayerHello(int user, string name);
            void ServerPlayerJoined(int user, string name);
            void ServerPlayerLeft(int user, string name);

            void PlayerAdvertise(int ego);
            void ServerPlayerAdvertise(int user, string name, int ego);

            // ...
        }
    }
}
