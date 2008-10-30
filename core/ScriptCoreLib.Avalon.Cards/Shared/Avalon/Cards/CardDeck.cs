using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows;

namespace ScriptCoreLib.Shared.Avalon.Cards
{
	[Script]
	public class CardDeck
	{

		public readonly BindingList<CardStack> Stacks = new BindingList<CardStack>();


		public CardDeck()
		{
			Stacks.ListChanged +=
				(sender, args) =>
				{
					if (args.ListChangedType == ListChangedType.ItemAdded)
					{
						Stacks[args.NewIndex].CurrentDeck = this;
						return;
					}
				};
		}



		
	}
}
