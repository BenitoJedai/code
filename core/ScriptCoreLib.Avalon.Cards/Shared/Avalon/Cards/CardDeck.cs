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
		public Func<CardInfo.RankEnum, int> GetRank;

		public Canvas Container { get; set; }
		public Canvas Content { get; set; }
		public Canvas Overlay { get; set; }

		public Sounds Sounds = new Sounds();

		public readonly BindingList<CardStack> Stacks = new BindingList<CardStack>();

		public readonly List<CardInfo> UnusedCards = new List<CardInfo>();

		public string CardCustomBackground;

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

		public Card[] FetchCards(int count)
		{
			var a = new List<Card>();

			while (count > 0)
			{
				var c = FetchCard;

				if (c != null)
				{
					a.Add(c);
				}

				count--;
			}

			return a.ToArray();
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

		public readonly FutureStream AnimatedMoveToChain = new FutureStream();


		public CardDeck()
		{
			const int DefaultWidth = 200;
			const int DefaultHeight = 200;

			#region set the chain to be signalled
			this.AnimatedMoveToChain.Continue(
				SignalNext =>
				{
					SignalNext();
				}
			)();
			#endregion


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
			var s = new List<CardStack>();

			p.ListChanged +=
				(sender, args) =>
				{
					if (args.ListChangedType == ListChangedType.ItemAdded)
					{
						var c = p[args.NewIndex];

						s.Add(c);

						this.Stacks.Add(c);
						return;
					}

					if (args.ListChangedType == ListChangedType.ItemDeleted)
					{
						var c = s[args.NewIndex];

						s.Remove(c);

						this.Stacks.Remove(c);
						return;
					}
				};

			return p;
		}


	}
}
