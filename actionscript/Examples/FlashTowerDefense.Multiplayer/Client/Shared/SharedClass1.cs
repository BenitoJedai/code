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


            // client -> server
            void CancelServerRandomNumbers();
            void ReadyForServerRandomNumbers();

            // client -> server  -> others
            void TakeBox(int box);
            // server -> others
            void UserTakeBox(int user, int box);

            void FiredShotgun();
            void UserFiredShotgun(int user);
            // ...
        }


#if !NoAttributes
        [Script]
#endif
        [CompilerGenerated]
        public enum Messages
        {
            None = 100,

            TeleportTo,
            UserTeleportTo,

            FiredShotgun,
            UserFiredShotgun,


            Ping = 200,
            Pong,

            UserJoined,
            ToUserJoinedReply,  // client->server
            UserJoinedReply,    // server->client

            UserLeft,

            UserEnterMachineGun,
            UserExitMachineGun,

            UserStartMachineGun,
            UserStopMachineGun,


            UserWalkTo,


            EnterMachineGun,
            ExitMachineGun,

            StartMachineGun,
            StopMachineGun,



            WalkTo,

            ServerMessage,
            ServerRandomNumbers,

            ReadyForServerRandomNumbers,
            CancelServerRandomNumbers,

            AddDamageFromDirection,
            UserAddDamageFromDirection,


            // for others
            TakeBox,
            UserTakeBox,


            ShowBulletsFlying,
            UserShowBulletsFlying,
        }

    }
}
