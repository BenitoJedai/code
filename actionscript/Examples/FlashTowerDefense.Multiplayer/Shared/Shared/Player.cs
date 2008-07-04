using System;
using System.Collections.Generic;
using System.Text;
#if !NoAttributes
using ScriptCoreLib;
#endif
namespace FlashTowerDefense.Shared
{
#if !NoAttributes
    [Script]
#endif
    public class Player : Generic.ServerPlayerBase<SharedClass1.RemoteEvents, SharedClass1.RemoteMessages>
    {
        public enum GameEventStatusEnum
        {
            Unknown,
            Ready,
            Cancelled,
            Pending,
            Lagging
        }

        public GameEventStatusEnum GameEventStatus = GameEventStatusEnum.Unknown;

        public DateTime LastMessage = DateTime.MinValue;

    }
}
