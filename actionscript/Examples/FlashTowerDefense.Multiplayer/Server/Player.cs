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
            Pending
        }

        public GameEventStatusEnum GameEventStatus;

        
    }
}
