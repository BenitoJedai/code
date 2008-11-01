using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Media;

namespace ScriptCoreLib.Shared.Avalon.Cards
{
	[Script]
	public class CardDrag
	{


		public CardDrag(Card card)
		{

			var offset = new Point();
			var drag = false;

			var OverlayContainer = card.CurrentDeck.Overlay;

			card.Overlay.MouseLeftButtonDown +=
				(sender, args) =>
				{
					if (card.VisibleSide == Card.SideEnum.BackSide)
						return;

					offset = args.GetPosition(card.Overlay);
					drag = true;

					card.BringToFront();


					foreach (var v in card.CurrentDeck.Cards)
					{
						if (v != card)
						{
							v.Overlay.Fill = Brushes.Yellow;
							v.Overlay.Hide();
						}
					}
					card.Overlay.Fill = Brushes.White;
					
				};

			OverlayContainer.MouseMove +=
				(sender, args) =>
				{
					if (drag)
					{
						var p = args.GetPosition(OverlayContainer) - offset;


						card.MoveTo(Convert.ToInt32(p.X), Convert.ToInt32(p.Y));
					}
				};

			OverlayContainer.MouseLeftButtonUp +=
				delegate
				{
					if (drag)
					{
						drag = false;


						foreach (var v in card.CurrentDeck.Cards)
						{
							if (v != card)
							{
								v.Overlay.Show();
							}
						}

						card.Overlay.Orphanize();
						card.Overlay.AttachTo(OverlayContainer);

					}
				};
		}
	}

}
