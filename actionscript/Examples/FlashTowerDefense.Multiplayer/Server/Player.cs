using System;
using System.Collections.Generic;
using System.Text;

using Nonoba.GameLibrary;
using FlashTowerDefense.Shared;

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

        internal SharedClass1.RemoteEvents NetworkEvents;
        internal SharedClass1.RemoteMessages NetworkMessages;


        public override Dictionary<string, string> GetDebugValues()
        {
            return new Dictionary<string, string> { };
        }
    }
}
