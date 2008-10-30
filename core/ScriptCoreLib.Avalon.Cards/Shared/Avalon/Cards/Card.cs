using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Controls;
using ScriptCoreLib.Shared.Avalon.Tween;

namespace ScriptCoreLib.Shared.Avalon.Cards
{
	[Script]
	public partial class Card : ISupportsContainer
	{
		public readonly CardInfo Info;

		public Canvas Container { get; set; }

		public readonly CardDeck CurrentDeck;

		public CardStack CurrentStack;

		public static implicit operator CardStack(Card e)
		{
			return e.CurrentStack;
		}


		public event Action Click;
		public event Action DoubleClick;
		public event Action Moved;

		public event Func<CardStack, bool> ValidateDragStop;




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
					return;
				}

				ImageTopSide.Visibility = System.Windows.Visibility.Hidden;
				ImageBackSide.Visibility = System.Windows.Visibility.Visible;
			}
		}

		public Card(CardDeck d, CardInfo i)
		{
			this.Container = new Canvas
			{
				Width = CardInfo.Width,
				Height = CardInfo.Height
			};

			this.ImageTopSide = new Image
			{
				Source = i.GetImagePath(KnownAssets.Path.DefaultCards).ToSource(),
				Width = CardInfo.Width,
				Height = CardInfo.Height
			}.AttachTo(this);

			this.ImageBackSide = new Image
			{
				Source = CardInfo.GetImagePath(KnownAssets.Path.DefaultCards, false, 0, null, false).ToSource(),
				Width = CardInfo.Width,
				Height = CardInfo.Height,
			}.AttachTo(this);

			this.VisibleSide = SideEnum.TopSide;

			CurrentDeck = d;

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

			this.Container.MouseLeftButtonUp +=
				//Control.onclick += delegate(IEvent e)
				delegate
				{
					if (Click != null)
						Click();

					//if (CurrentStack != null)
					//    CurrentStack.RaiseClick(this);
				};



			//Opacity = 1;

			//Enabled = false;
		}
	}
}
