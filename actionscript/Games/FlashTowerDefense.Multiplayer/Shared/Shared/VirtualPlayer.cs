using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Nonoba.Generic;

namespace FlashTowerDefense.Shared
{
    [Script]
    public class VirtualPlayer : ServerPlayerBase<SharedClass1.IEvents, SharedClass1.IMessages>
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

        //public DateTime LastMessage = DateTime.MinValue;
        public DateTime LastMessage = DateTime.Now;

    }
}
