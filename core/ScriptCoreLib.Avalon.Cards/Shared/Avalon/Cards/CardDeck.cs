using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.Shared.Lambda;
using System.Windows.Media;

namespace ScriptCoreLib.Shared.Avalon.Cards
{
	[Script]
	public class CardDeck : ISupportsContainer
	{
		public Canvas Container { get; set; }
		public Canvas Content { get; set; }
		public Canvas Overlay { get; set; }

		public readonly BindingList<CardStack> Stacks = new BindingList<CardStack>();

		public readonly List<CardInfo> UnusedCards = new List<CardInfo>();

		public CardInfo Fetch
		{
			get
			{
				if (UnusedCards.Count == 0)
					return null;


				var x = UnusedCards.Random();

				UnusedCards.Remove(x);

				return x;
			}
		}

		/// <summary>
		/// this event will be invoked at the moment a new card is created
		/// </summary>
		public event Action<Card> ApplyCardRules;

		public readonly List<Card> Cards = new List<Card>();

		public Card FetchCard
		{
			get
			{
				var i = this.Fetch;

				if (i != null)
				{
					var c = new Card(this, i);

					if (ApplyCardRules != null)
						ApplyCardRules(c);

					Cards.Add(c);

					return c;
				}

				return null;
			}
		}

		public void SizeTo(int x, int y)
		{
			this.Container.Width = x;
			this.Container.Height = y;

			this.Content.Width = x;
			this.Content.Height = y;


			this.Overlay.Width = x;
			this.Overlay.Height = y;
		}

		public CardDeck()
		{
			const int DefaultWidth = 200;
			const int DefaultHeight = 200;

			this.Container = new Canvas { Width = DefaultWidth, Height = DefaultHeight };

			this.Content =
				new Canvas
				{
					Width = DefaultWidth,
					Height = DefaultHeight,
				}.AttachTo(this);

			this.Overlay =
				new Canvas
				{
					Background = Brushes.Gray,
					Width = DefaultWidth,
					Height = DefaultHeight,
					Opacity = 0
				}.AttachTo(this);


			Stacks.ListChanged +=
				(sender, args) =>
				{
					if (args.ListChangedType == ListChangedType.ItemAdded)
					{
						var value = Stacks[args.NewIndex];

						value.CurrentDeck.Value = this;
						value.OrphanizeContainer();
						value.AttachContainerTo(this.Content);
						value.Update();

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
