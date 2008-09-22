using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Shared.Avalon.Extensions;
using Mahjong.Shared;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Input;
using System.IO;

namespace Mahjong.Code
{
	partial class VisibleLayout
	{
		private void UpdateRelations(VisibleTile e)
		{
			e.Entry.Siblings.ForEach(k => k.FindSiblings(this.TilesInfo.TilesByPointer));
		}

		public void Remove(VisibleTile a, VisibleTile b)
		{
			Remove(a, b, true);
		}

		readonly FutureLock FlashLock = new FutureLock();

		public void FlashGreen(VisibleTile[] a)
		{
			FlashGreen(a,
				delegate
				{
					100.AtDelay(() => FlashGreen(a, null));
				}
			);
		}

		public void FlashGreen(VisibleTile[] a, Action Done)
		{
			FlashLock.Acquire(
				delegate
				{
					foreach (var b in a)
					{
						b.BlackFilter.Visibility = System.Windows.Visibility.Hidden;
						b.GreenFilter.Opacity = 0.6;

					}


					200.AtDelay(
						delegate
						{
							foreach (var b in a)
							{
								b.BlackFilter.Visibility = System.Windows.Visibility.Visible;
								b.GreenFilter.Opacity = 0;

							}

							FlashLock.Release();

							if (Done != null)
								Done();
						}
					);
				}
			);
		}

		public void Remove(VisibleTile a, VisibleTile b, bool IsLocalPlayer)
		{
			FlashLock.Acquire(
				delegate
				{
					a.BlackFilter.Visibility = System.Windows.Visibility.Hidden;
					b.BlackFilter.Visibility = System.Windows.Visibility.Hidden;

					a.Control.Opacity = 1;
					b.Control.Opacity = 1;

					a.GreenFilter.Opacity = 0;
					b.GreenFilter.Opacity = 0;

					a.YellowFilter.Opacity = 0.6;
					b.YellowFilter.Opacity = 0.6;

					200.AtDelay(
						delegate
						{
							a.BlackFilter.Visibility = System.Windows.Visibility.Visible;
							b.BlackFilter.Visibility = System.Windows.Visibility.Visible;


							a.YellowFilter.Opacity = 0;
							b.YellowFilter.Opacity = 0;

							a.Visible = false;
							b.Visible = false;

							UpdateRelations(a);
							UpdateRelations(b);

							if (this.Tiles.Any(k => k.Visible))
							{
								GoBackHistory.Push(
									new RemovedTilePair
									{
										Left = a,
										Right = b
									}
								);


								if (GoBackHistory.Count == 1)
									if (GoBackAvailable != null)
										GoBackAvailable();

								GoForwardHistory.Clear();

								if (GoForwardUnavailable != null)
									GoForwardUnavailable();
							}
							else
							{
								GoBackHistory.Clear();

								if (GoBackUnavailable != null)
									GoBackUnavailable();

								GoForwardHistory.Clear();

								if (GoForwardUnavailable != null)
									GoForwardUnavailable();

								if (ReadyForNextLayout != null)
									ReadyForNextLayout(IsLocalPlayer);
							}

							FlashLock.Release();
						}
					);
				}
			);
		}

		public event Action<bool> ReadyForNextLayout;

		public event Action GoForwardAvailable;
		public event Action GoForwardUnavailable;
		public event Action GoBackAvailable;
		public event Action GoBackUnavailable;


		public event Action GoBackCompleted;
		public event Action GoForwardCompleted;

		public void GoBack()
		{
			if (GoBackHistory.Count == 0)
				return;

			var p = GoBackHistory.Pop();

			p.Left.Visible = true;
			p.Right.Visible = true;

			UpdateRelations(p.Left);
			UpdateRelations(p.Right);

			if (GoBackHistory.Count == 0)
				if (GoBackUnavailable != null)
					GoBackUnavailable();

			GoForwardHistory.Push(p);

			if (GoForwardHistory.Count == 1)
				if (GoForwardAvailable != null)
					GoForwardAvailable();

			if (GoBackCompleted != null)
				GoBackCompleted();

		}

		public void GoForward()
		{
			if (GoForwardHistory.Count == 0)
				return;

			var p = GoForwardHistory.Pop();

			p.Left.Visible = false;
			p.Right.Visible = false;

			UpdateRelations(p.Left);
			UpdateRelations(p.Right);

			if (GoForwardHistory.Count == 0)
				if (GoForwardUnavailable != null)
					GoForwardUnavailable();

			GoBackHistory.Push(p);

			if (GoBackHistory.Count == 1)
				if (GoBackAvailable != null)
					GoBackAvailable();

			if (GoForwardCompleted != null)
				GoForwardCompleted();
		}

		[Script]
		public class RemovedTilePair
		{
			public VisibleTile Left;
			public VisibleTile Right;
		}

		public readonly Stack<RemovedTilePair> GoBackHistory = new Stack<RemovedTilePair>();
		public readonly Stack<RemovedTilePair> GoForwardHistory = new Stack<RemovedTilePair>();

	}
}
