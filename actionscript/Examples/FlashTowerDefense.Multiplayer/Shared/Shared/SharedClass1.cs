using System;
using System.Collections.Generic;
using System.Text;


namespace FlashTowerDefense.Shared
{
    public partial class SharedClass1
    {
        // members defined over here can be used on client and on server
        // x

        public string Hello;

        public enum Messages
        {
            None = 100,
            
            UserJoined,
            ToUserJoinedReply,  // client->server
            UserJoinedReply,    // server->client

            UserLeft,

            UserEnterMachineGun,
            UserExitMachineGun,

            UserStartMachineGun,
            UserStopMachineGun,

            UserTeleportTo,
            UserWalkTo,
            UserFiredShotgun,

            EnterMachineGun,
            ExitMachineGun,

            StartMachineGun,
            StopMachineGun,

            TeleportTo,
            WalkTo,
            FiredShotgun,

            ServerMessage,
            ServerRandomNumbers,

            ReadyForServerRandomNumbers,
            CancelServerRandomNumbers,

            AddDamageFromDirection,
            UserAddDamageFromDirection,

            TakeBox,
            UserTakeBox
        }


    }
}
