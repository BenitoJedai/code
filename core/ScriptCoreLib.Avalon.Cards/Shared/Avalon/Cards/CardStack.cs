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
		public event Action<Card> Click;

		public void RaiseClick(Card c)
		{
			if (Click != null)
				Click(c);
		}

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
			}.AttachTo(this);

			this.Cards.ListChanged +=
				(sender, args) =>
				{
					if (args.ListChangedType == ListChangedType.ItemAdded)
						this.Cards[args.NewIndex].CurrentStack = this;

				

				};
		}

		public Point Location
		{
			get
			{
				return new Point { X = LocationX, Y = LocationY };
			}
		}

		public Point FreeLocation
		{
			get
			{
				return new Point
				{
					X = LocationX + CardMargin.X * (Cards.Count ),
					Y = LocationY + CardMargin.Y * (Cards.Count ),
				};
			}
		}

		public int LocationX;
		public int LocationY;

		public CardStack MoveTo(int x, int y)
		{
			LocationX = x;
			LocationY = y;

			this.Container.MoveTo(x, y);


			Update();

			return this;
		}

		public int HitRange = 32;

		public Vector CardMargin = new Vector { X = 0, Y = 12 };

		public void Update()
		{
			if (this.CurrentDeck.Value != null)
			{
				this.Cards.ForEach(
					(Card c, int index) =>
					{
						c.BringToFront();

						c.MoveTo(
							Convert.ToInt32(LocationX + CardMargin.X * index)
							,
							Convert.ToInt32(LocationY + CardMargin.Y * index)
						);
					}
				);
			}


			return;
		}


		public void Add(Card value)
		{
			Cards.Add(value);
		}

		public void Add(Card[] value)
		{
			value.ForEach(Add);
		}

		public void RevealLastCard()
		{
			if (this.Cards.Count == 0)
				return;

			var last = this.Cards.Last();

			if (last.VisibleSide == Card.SideEnum.BackSide)
				last.VisibleSide = Card.SideEnum.TopSide;
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
