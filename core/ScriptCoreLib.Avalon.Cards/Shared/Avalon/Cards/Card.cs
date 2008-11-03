using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Controls;
using ScriptCoreLib.Shared.Avalon.Tween;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Input;
using ScriptCoreLib.Shared.Lambda;
using System.Windows;

namespace ScriptCoreLib.Shared.Avalon.Cards
{
	[Script]
	public partial class Card : ISupportsContainer
	{
		public readonly CardInfo Info;

		public Canvas Container { get; set; }

		public Rectangle Overlay { get; set; }

		public readonly CardDeck CurrentDeck;

		public CardStack CurrentStack;

		public static implicit operator CardStack(Card e)
		{
			return e.CurrentStack;
		}

		public override string ToString()
		{
			return this.Info.Description;
		}

		public event Action Click;
		public event Action DoubleClick;
		public event Action Moved;

		public void AttachToStack(CardStack s)
		{
			var c = this;

			if (c.CurrentStack != null)
			{
				c.CurrentStack.Cards.Remove(c);
			}

			c.CurrentStack = s;

			s.Cards.Add(c);

			if (this.Moved != null)
				this.Moved();
		}

		public Func<bool> ValidateDragStart;
		public Func<CardStack, bool> ValidateDragStop;

		public int Rank
		{
			get
			{
				if (this.CurrentDeck.GetRank == null)
					return (int)Info.Rank;

				return this.CurrentDeck.GetRank(Info.Rank);
			}
		}


		public Image ImageTopSide;
		public Image ImageBackSide;

		public enum SideEnum
		{
			TopSide,
			BackSide
		}

		SideEnum _VisibleSide;
		public SideEnum VisibleSide
		{
			get
			{
				return _VisibleSide;
			}
			set
			{
				_VisibleSide = value;

				if (value == SideEnum.TopSide)
				{
					ImageTopSide.Visibility = System.Windows.Visibility.Visible;
					ImageBackSide.Visibility = System.Windows.Visibility.Hidden;
					this.Overlay.Cursor = Cursors.Hand;
					return;
				}

				ImageTopSide.Visibility = System.Windows.Visibility.Hidden;
				ImageBackSide.Visibility = System.Windows.Visibility.Visible;
				this.Overlay.Cursor = Cursors.Arrow;

			}
		}

		public readonly CardDrag Drag;

		public Card(CardDeck deck, CardInfo i)
		{
			if (deck == null)
				throw new ArgumentNullException("deck");

			this.Container = new Canvas
			{
				Width = CardInfo.Width,
				Height = CardInfo.Height
			};

			this.Overlay = new Rectangle
			{
				Fill = Brushes.White,
				Width = CardInfo.Width,
				Height = CardInfo.Height
			};



			string ImageBackSideSource = deck.CardCustomBackground;

			if (ImageBackSideSource == null)
				ImageBackSideSource = CardInfo.GetImagePath(KnownAssets.Path.DefaultCards, false, 0, null, false);

			this.ImageBackSide = new Image
			{
				Source = ImageBackSideSource.ToSource(),
				Width = CardInfo.Width,
				Height = CardInfo.Height,
			}.AttachTo(this);

			i.Visible = true;
			this.ImageTopSide = new Image
			{
				Source = i.GetImagePath(KnownAssets.Path.DefaultCards).ToSource(),
				Width = CardInfo.Width,
				Height = CardInfo.Height
			}.AttachTo(this);


			this.VisibleSide = SideEnum.BackSide;

			CurrentDeck = deck;



			this.Overlay.AttachTo(deck.Overlay);

			this.Drag = new CardDrag(this);

			//Drag = new DragHelper(Control);
			//OpacityHelper = new TweenDataDouble(
			//    delegate
			//    {
			//        foreach (Card v in this.MovableCards)
			//        {
			//            v.Control.style.Opacity = Opacity;
			//        }
			//    }
			//    );

			Info = i;

			//Control.style.cursor = IStyle.CursorEnum.@default;
			//Control.style.SetSize(CardInfo.Width, CardInfo.Height);



			//Native.Document.body.appendChild(Control);

			//MoveTo(Drag.Position);

			//Drag.History = new System.Collections.Generic.List<Point>();

			//Drag.DragStop +=
			//    delegate
			//    {
			//        Opacity = 1;

			//        var s = FindStack();

			//        if (s == null)
			//            MoveTo(Drag.History.First());
			//        else
			//        {
			//            s.AttachCards(this.MovableCards);
			//        }

			//        Drag.History.Clear();
			//    };

			//Drag.DragStart +=
			//    delegate
			//    {
			//        Opacity = 0.7;

			//        foreach (Card v in MovableCards)
			//        {
			//            v.BringToFront();
			//        }
			//    };

			//Drag.DragMove +=
			//    delegate
			//    {



			//        var s = FindStack();

			//        if (s != null)
			//        {
			//            Opacity = 1;
			//            MoveTo(s.NextPosition);
			//        }
			//        else
			//        {
			//            Opacity = 0.7;
			//            MoveTo(Drag.Position);
			//        }





			//    };



			//Control.ondblclick += (e) => Helper.Invoke(DoubleClick);

			this.Overlay.MouseLeftButtonUp +=
				//Control.onclick += delegate(IEvent e)
				delegate
				{
					this.CurrentDeck.Sounds.click();

					if (Click != null)
						Click();

					if (CurrentStack != null)
						CurrentStack.RaiseClick(this);
				};


			this.AnimatedOpacity = 1;

			//Opacity = 1;

			//Enabled = false;
		}

		public int LocationX;
		public int LocationY;

		public int ApprovedLocationX;
		public int ApprovedLocationY;

		public Point LocationInStack
		{
			get
			{
				var p = new Point();

				var s = this.CurrentStack;

				if (s != null)
				{
					p.X = s.LocationX + s.CardMargin.X * s.Cards.IndexOf(this);
					p.Y = s.LocationY + s.CardMargin.Y * s.Cards.IndexOf(this);
				}
				return p;
			}
		}

		public void MoveSelectionTo(int x, int y)
		{
			this.MoveTo(x, y);

			this.StackedCards.ForEach(
				(k, index) =>
					k.MoveTo(
						Convert.ToInt32(x + CurrentStack.CardMargin.X * (index + 1)),
						Convert.ToInt32(y + CurrentStack.CardMargin.Y * (index + 1))
					)
			);
		}

		public void MoveTo(int x, int y)
		{

			this.LocationX = x;
			this.LocationY = y;

			this.Overlay.MoveTo(x, y);
			this.Container.MoveTo(x, y);

		}

		public bool AnimatedMoveToActive = false;

		public void AnimatedMoveTo(Point p)
		{
			AnimatedMoveTo(
				Convert.ToInt32( p.X), 
				Convert.ToInt32(p.Y)
			);
		}

		public void AnimatedMoveTo(int LocationX, int LocationY)
		{
			if (AnimatedMoveToActive)
				return;

			var ox = this.LocationX;
			var oy = this.LocationY;

			Action<int, int> tween = NumericEmitter.Of(
				(x, y) =>
				{
					if (!AnimatedMoveToActive)
						return;

					if (ox == this.LocationX)
						if (oy == this.LocationY)
						{
							this.MoveTo(x, y);
							ox = x;
							oy = y;
							if (x == LocationX)
								if (y == LocationY)
									AnimatedMoveToActive = false;

							return;
						}

					AnimatedMoveToActive = false;
				}
			);

			AnimatedMoveToActive = true;

			tween(ox, oy);
			tween(LocationX, LocationY);
		}

		public void BringToFront()
		{
			this.Container.Orphanize();
			this.Container.AttachTo(this.CurrentDeck.Content);


		}

		public IEnumerable<Card> SelectedCards
		{
			get
			{
				return new[] { this }.Concat(StackedCards);
			}
		}
		public Card[] StackedCards
		{
			get
			{
				var a = new System.Collections.Generic.List<Card>();

				if (CurrentStack != null)
				{
					var i = CurrentStack.Cards.IndexOf(this);

					if (i > -1)
					{
						for (int j = i + 1; j < CurrentStack.Cards.Count; j++)
						{
							a.Add(CurrentStack.Cards[j]);
						}
					}
				}

				return a.ToArray();
			}
		}
	}
}
