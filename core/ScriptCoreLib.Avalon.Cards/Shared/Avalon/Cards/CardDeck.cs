using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Controls;

namespace ScriptCoreLib.Shared.Avalon.Cards
{
	[Script]
	public class CardDeck : ISupportsContainer
	{
		public Canvas Container { get; set; }

		public readonly BindingList<CardStack> Stacks = new BindingList<CardStack>();


		public CardDeck()
		{
			Stacks.ListChanged +=
				(sender, args) =>
				{
					if (args.ListChangedType == ListChangedType.ItemAdded)
					{
						Stacks[args.NewIndex].CurrentDeck.Value = this;
						return;
					}
				};
		}


		/// <summary>
		///  creates a new list of cardstacks, and makes sure that each card
		///  will be added to the decks stack list as backreference
		/// </summary>
		/// <returns></returns>
		public BindingList<CardStack> CreateStackList()
		{
			var p = new BindingList<CardStack>();

			p.ListChanged +=
				(sender, args) =>
				{
					if (args.ListChangedType == ListChangedType.ItemAdded)
					{
						//System.Diagnostics.Debugger.Break();
						this.Stacks.Add(p[args.NewIndex]);
						return;
					}

					if (args.ListChangedType == ListChangedType.ItemDeleted)
					{
						//System.Diagnostics.Debugger.Break();
						// sync?

						this.Stacks.RemoveAt(args.NewIndex);
						return;
					}
				};

			return p;
		}

		
	}
}
