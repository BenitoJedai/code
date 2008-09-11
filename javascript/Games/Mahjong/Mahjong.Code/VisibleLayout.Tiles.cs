using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using Mahjong.Shared;

namespace Mahjong.Code
{
	partial class VisibleLayout
	{
		[Script]
		public class TilesInfoType
		{
			public Entry[] Tiles;
			public Entry[] TilesByPointer;

			public readonly int CountZ;

			public TilesInfoType(int CountZ, Layout.Entry[] source)
			{
				this.CountZ = CountZ;
				this.TilesByPointer = new Entry[CountZ * Layout.DefaultCountY * Layout.DefaultCountX];

				// upgrade the tile entries so we could bind images
				// must preserve ordering
				this.Tiles = source.Select(
					k =>
					{
						var x = k as Entry;

						var n = new Entry
							{
								index = k.index,
								x = k.x,
								y = k.y,
								z = k.z,
							};

						if (x != null)
						{
							n.RankImage = x.RankImage;
							n.Visible = x.Visible;
						}

						this.TilesByPointer[n.Pointer] = n;

						return n;
					}
				).ToArray();

				FindSiblings();
			}

			public void FindSiblings()
			{
				foreach (var v in this.Tiles)
				{
					v.FindSiblings(this.TilesByPointer);
				}
			}
		}

		TilesInfoType TilesInfo;

		public Entry[] Tiles
		{
			get
			{
				return TilesInfo.Tiles;
			}
		}
	}
}
