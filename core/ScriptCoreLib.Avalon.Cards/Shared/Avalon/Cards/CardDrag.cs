using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Media;
using ScriptCoreLib.Shared.Lambda;

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
					if (drag)
						return;

					if (card.VisibleSide == Card.SideEnum.BackSide)
						return;

					if (card.SelectedCards.Any(k => k.AnimatedMoveToActive))
						return;

					if (!card.ValidateDragStart())
						return;

					offset = args.GetPosition(card.Overlay);
					drag = true;



					card.SelectedCards.ForEach(
						k =>
						{
							k.BringToFront();
							k.ApprovedLocationX = k.LocationX;
							k.ApprovedLocationY = k.LocationY;
							k.AnimatedOpacity = 0.7;
						}
					);

					foreach (var v in card.CurrentDeck.Cards)
					{
						if (v != card)
						{
							v.Overlay.Fill = Brushes.Yellow;
							v.Overlay.Hide();
						}
					}

					card.Overlay.Fill = Brushes.White;

					card.CurrentDeck.Sounds.drag();

				};

			var CandidateStack = default(CardStack);

			OverlayContainer.MouseMove +=
				(sender, args) =>
				{
					if (drag)
					{
						var v = args.GetPosition(OverlayContainer) - offset;
						var x = Convert.ToInt32(v.X);
						var y = Convert.ToInt32(v.Y);
						var p = new Point { X = x, Y = y };


						var CandidateStack_ = card.CurrentDeck.Stacks.Where(k => k != card.CurrentStack).FirstOrDefault(
							StackToBeChecked =>
							{
								var Length = (StackToBeChecked.FreeLocation - p).Length;

								return Length < StackToBeChecked.HitRange;
							}
						);

						if (CandidateStack != null)
							if (CandidateStack_ != null)
								if (CandidateStack == CandidateStack_)
									return;

						CandidateStack = CandidateStack_;

						if (CandidateStack != null)
							if (!card.ValidateDragStop(CandidateStack))
								CandidateStack = null;

						if (CandidateStack == null)
						{
							card.AnimatedOpacity = 0.7;
							card.MoveSelectionTo(x, y);
						}
						else
						{
							Console.WriteLine(new { x, y, CandidateStack, card.CurrentStack });

							card.AnimatedOpacity = 1;

							var f = CandidateStack.FreeLocation;

							card.MoveSelectionTo(
								Convert.ToInt32(f.X),
								Convert.ToInt32(f.Y)
							);
						}

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

						card.SelectedCards.ForEach(
							k =>
							{
								k.Overlay.Orphanize();
								k.Overlay.AttachTo(OverlayContainer);

								if (CandidateStack == null)
								{
									k.AnimatedMoveTo(k.ApprovedLocationX, k.ApprovedLocationY);
								}
								else
								{
									k.AttachToStack(CandidateStack);
								}

								k.AnimatedOpacity = 1;
							}
						);


					}
				};
		}
	}

}
