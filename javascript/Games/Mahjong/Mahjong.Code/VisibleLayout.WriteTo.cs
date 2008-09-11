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
		public void WriteTo(Stream m)
		{
			if (Layout == null)
				return;

			var w = new BinaryWriter(m);

			// what layout are we using
			w.Write(this.Layout.DataString);

			w.Write(this.TilesInfo.Tiles.Length);

			foreach (var v in this.TilesInfo.Tiles)
			{
				w.Write((short)v.index);
				w.Write((byte)v.x);
				w.Write((byte)v.y);
				w.Write((byte)v.z);

				w.Write(v.RankImage.Rank);
				w.Write(v.RankImage.Suit);
				w.Write((byte)v.RankImage.IsPairableMode);

				w.Write((byte)v.Visible.ToByte());

			}

			Console.WriteLine(new { m.Length }.ToString());
		}

		public void ReadFrom(Stream m)
		{
			this.Layout = null;

			

			var r = new BinaryReader(m);

			var DataString = r.ReadString();

			var TilesCount = r.ReadInt32();
			var Tiles = Enumerable.Range(0, TilesCount).ToArray(
					i =>
					{
						var n =
							new Entry
							{
								index = r.ReadInt16(),
								x = r.ReadByte(),
								y = r.ReadByte(),
								z = r.ReadByte(),
							};

						n.RankImage = new RankAsset
						{
							Rank = r.ReadString(),
							Suit = r.ReadString(),
							IsPairableMode = (RankAsset.IsPairableModeEnum)r.ReadByte()
						};

						n.Visible = r.ReadByte().ToBoolean();


						return n;
					}
				);

			var layout = new Layout(DataString, Tiles);

			this._Layout = layout;

			this.LayoutProgress = new Future();

			this.TilesInfo = new TilesInfoType(this.Layout.CountZ, this.Layout.Tiles);

			if (LayoutChanging != null)
				LayoutChanging();

			
	
			
			LayoutUpdateFinalize();
		}
	}
}
