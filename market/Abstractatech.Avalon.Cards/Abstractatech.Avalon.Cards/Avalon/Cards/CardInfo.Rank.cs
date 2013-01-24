using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.Avalon.Cards
{
	partial class CardInfo
	{
		public enum RankEnum
		{
			Unknown,

			RankAce,
			RankKing,
			RankQueen,
			RankJack,
			Rank10,
			Rank9,
			Rank8,
			Rank7,
			Rank6,
			Rank5,
			Rank4,
			Rank3,
			Rank2,
			RankJoker,
		}

		public RankEnum Rank;
	}
}
