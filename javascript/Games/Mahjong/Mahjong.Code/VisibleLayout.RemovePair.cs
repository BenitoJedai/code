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

			RemovedTiles.Push(
				new RemovedTilePair
				{
					Left = a,
					Right = b
				}
			);

			if (RemovedTiles.Count == 1)
				if (GoBackAvailable != null)
					GoBackAvailable();

			RemovedTilesForRedo.Clear();

			if (GoForwardUnavailable != null)
				GoForwardUnavailable();
		}

		public event Action GoForwardAvailable;
		public event Action GoForwardUnavailable;
		public event Action GoBackAvailable;
		public event Action GoBackUnavailable;

		public void GoBack()
		{
			if (RemovedTiles.Count == 0)
				return;

			var p = RemovedTiles.Pop();

			p.Left.Visible = true;
			p.Right.Visible = true;

			UpdateRelations(p.Left);
			UpdateRelations(p.Right);

			if (RemovedTiles.Count == 0)
				if (GoBackUnavailable != null)
					GoBackUnavailable();

			RemovedTilesForRedo.Push(p);

			if (RemovedTilesForRedo.Count == 1)
				if (GoForwardAvailable != null)
					GoForwardAvailable();
		}

		public void GoForward()
		{
			if (RemovedTilesForRedo.Count == 0)
				return;

			var p = RemovedTilesForRedo.Pop();

			p.Left.Visible = false;
			p.Right.Visible = false;

			UpdateRelations(p.Left);
			UpdateRelations(p.Right);

			if (RemovedTilesForRedo.Count == 0)
				if (GoForwardUnavailable != null)
					GoForwardUnavailable();

			RemovedTiles.Push(p);

			if (RemovedTiles.Count == 1)
				if (GoBackAvailable != null)
					GoBackAvailable();
		}

		[Script]
		public class RemovedTilePair
		{
			public VisibleTile Left;
			public VisibleTile Right;
		}

		public readonly Stack<RemovedTilePair> RemovedTiles = new Stack<RemovedTilePair>();
		public readonly Stack<RemovedTilePair> RemovedTilesForRedo = new Stack<RemovedTilePair>();

	}
}
