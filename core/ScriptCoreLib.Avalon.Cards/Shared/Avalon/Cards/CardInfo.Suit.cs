using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.Avalon.Cards
{
	partial class CardInfo
	{
		public enum SuitEnum
		{
			Unknown,

			Spade,
			Club,
			Heart,
			Diamond,

		}

		public SuitEnum Suit;
	}
}
