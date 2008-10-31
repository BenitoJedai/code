using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.Shared.Lambda;

namespace ScriptCoreLib.Shared.Avalon.Cards
{
	[Script]
	public class CardStack : ISupportsContainer, IEnumerable<Card>
	{
		public readonly Future<CardDeck> CurrentDeck = new Future<CardDeck>();

		public Canvas Container { get; set; }
		public readonly BindingList<Card> Cards = new BindingList<Card>();

		public CardStack()
		{
			this.Container = new Canvas
			{
				Width = CardInfo.Width,
				Height = CardInfo.Height
			};

			new Image
			{
				Source = CardInfo.GetImagePath(KnownAssets.Path.DefaultCards, false, 0, "spider.empty", false).ToSource(),

				Width = CardInfo.Width,
				Height = CardInfo.Height
			}.AttachTo(Container);

			this.Cards.ListChanged +=
				(sender, args) =>
				{
					if (args.ListChangedType == ListChangedType.ItemAdded)
					{
						var value = this.Cards[args.NewIndex];

						CurrentDeck.Continue(
							delegate
							{
								value.OrphanizeContainer();
								value.AttachContainerTo(this.CurrentDeck.Value);
								
							}
						);
							
					}
				};
		}

		int LocationX;
		int LocationY;

		public CardStack MoveTo(int x, int y)
		{
			LocationX = x;
			LocationY = y;

			this.Container.MoveTo(x, y);



			return Update();
		}

		public CardStack Update()
		{
		
			this.Cards.ForEach(
				(Card c, int index) =>
				{
					c.Container.MoveTo(
						LocationX + 0, LocationY + 12 * index
					);
				}
			);


			return this;
		}

		public void Add(Card value)
		{
			Cards.Add(value);
		}

		public void Add(Card[] value)
		{
			value.ForEach(Add);
		}


		#region IEnumerable<Card> Members

		public IEnumerator<Card> GetEnumerator()
		{
			return this.Cards.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.Cards.GetEnumerator();
		}

		#endregion
	}
}
