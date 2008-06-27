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


        #region generated from IMessages
#if !NoAttributes
        [Script]
#endif
        [CompilerGenerated]
        public enum Messages
        {
            None = 100,

            Ping,
            Pong,

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


            // for others
            TakeBox,
            UserTakeBox,


            ShowBulletsFlying,
            UserShowBulletsFlying,
        }

#if !NoAttributes
        [Script]
#endif
        [CompilerGenerated]
        public partial class RemoteEvents
        {

            #region TeleportTo
#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed class TeleportToArguments
            {
                public int x;
                public int y;
            }

            public event Action<TeleportToArguments> TeleportTo;
            #endregion

            //public event Action<int, int, int> UserTeleportTo;
        }


#if !NoAttributes
        [Script]
#endif
        [CompilerGenerated]
        public class RemoteMessages : IMessages
        {

#if !NoAttributes
            [Script]
#endif
            [CompilerGenerated]
            public sealed class SendArguments
            {
                public Messages i;
                public object[] args;
            }


            public Action<SendArguments> Send;


            public void TeleportTo(int x, int y)
            {
                Send(new SendArguments { i = Messages.TeleportTo, args = new object[] { x, y } });
            }

            public void UserTeleportTo(int user, int x, int y)
            {
                Send(new SendArguments { i = Messages.UserTeleportTo, args = new object[] { user, x, y } });
            }

        }


        #endregion

    }
}
