using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jsc.meta.Library.Mashups
{
	[global::System.AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
	sealed class MonetizationAttribute : Attribute
	{

		public int Prize;
	}

	static class MonetizationUseCase
	{
		class Player
		{
			public bool MoreArmorBought;

			public bool AppearBigger;
		}

		[Monetization(Prize = 40 /*€*/)]
		public static void BuyMoreArmor(
			this Player p,
			string ArmorType,
			Action YieldReturn)
		{
			// the rewriter injected code to ask user for cash
			// if he did not pay this code did not get called

			// lets add some value what the user payed for
			p.MoreArmorBought = true;

			// this method finished and now we should let 
			// the callee know about it
			YieldReturn();
		}

		public static void ByMoreArmor_Click(this Player p)
		{
			p.BuyMoreArmor("BetterArmor",
				delegate
				{
					if (p.MoreArmorBought)
					{
						// BetterArmor makes the player bigger
						p.AppearBigger = true;
					}
					else
					{
						// no cash? too bad...
						Console.Beep();
					}
				}
			);
		}
	}
}
