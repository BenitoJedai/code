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
					ReadyForNextLayout();
			}
		}

		public event Action ReadyForNextLayout;

		public event Action GoForwardAvailable;
		public event Action GoForwardUnavailable;
		public event Action GoBackAvailable;
		public event Action GoBackUnavailable;



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
