using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using Mahjong.Shared;
using ScriptCoreLib.Shared.Lambda;

namespace Mahjong.Code
{
	partial class VisibleLayout
	{
		[Script]
		public class Entry : Layout.Entry
		{
			public RankAsset RankImage;

			public readonly Future<VisibleTile> Tile = new Future<VisibleTile>();


			public Entry[] SiblingsAround;
			public Entry[] SiblingsBelow;
			public Entry[] SiblingsAbove;
			public Entry[] Siblings;

			public int Pointer
			{
				get
				{
					return GetPointer(x, y, z);
				}
			}

			public static int GetPointer(int x, int y, int z)
			{
				if (x < 0)
					return -1;

				if (x >= Layout.DefaultCountX)
					return -1;


				if (y < 0)
					return -1;

				if (y >= Layout.DefaultCountY)
					return -1;


				return z * Layout.DefaultCountY * Layout.DefaultCountX
						+ y * Layout.DefaultCountX
						+ x;
			}

			public void FindSiblings(Entry[] ByPointer)
			{
				Func<int, int, int, Entry> f =
					(x, y, z) => 
					{
						var p = GetPointer(x + this.x, y + this.y, z + this.z);
						
						if (p < 0)
							return null;

						if (p >= ByPointer.Length)
							return null;

						return ByPointer[p];
					};

				Func<int, Entry[]> q =
					z =>
					{
						var a = new List<Entry>();

						for (int x = -2; x <= 2; x++)
							for (int y = -2; y <= 2; y++)
							{
								var n = f(x, y, z);

								if (n != null)
									if (n != this)
										a.Add(n);
							}

						return a.ToArray();
					};

				this.SiblingsAround = q(0);
				this.SiblingsBelow = q(-1);
				this.SiblingsAbove = q(1);

				this.Siblings =
					this.SiblingsAround.Concat(this.SiblingsBelow).Concat(this.SiblingsAbove).ToArray();

			}

			public override string ToString()
			{
				return new { Pointer, index }.ToString();
			}
		}

	}
}
