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
		}

		public void UndoRemove()
		{
			if (RemovedTiles.Count == 0)
				return;

			var p = RemovedTiles.Pop();

			p.Left.Visible = true;
			p.Right.Visible = true;

			UpdateRelations(p.Left);
			UpdateRelations(p.Right);
		}

		[Script]
		public class RemovedTilePair
		{
			public VisibleTile Left;
			public VisibleTile Right;
		}

		public readonly Stack<RemovedTilePair> RemovedTiles = new Stack<RemovedTilePair>();

	}
}
