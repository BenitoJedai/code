using System;
using System.Collections.Generic;
using System.Text;

using Nonoba.GameLibrary;

namespace FlashTowerDefense.Server
{
    public class Player : NonobaGameUser
    {
        public enum GameEventStatusEnum
        {
            Unknown,
            Ready,
            Cancelled,
            Pending,
            Lagging
        }

        internal GameEventStatusEnum GameEventStatus = GameEventStatusEnum.Unknown;

        public DateTime LastMessage = DateTime.MinValue;
    }
}
