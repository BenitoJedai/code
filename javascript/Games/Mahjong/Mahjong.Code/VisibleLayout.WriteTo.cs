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

			//var w = new BinaryWriter(m);

			//// what layout are we using
			//w.Write(this.Layout.DataString.Length);
			//w.Write(this.Layout.DataString);


			//foreach (var v in this.TilesInfo.Tiles)
			//{
			//    w.Write((short)v.index);
			//    w.Write((byte)v.x);
			//    w.Write((byte)v.y);
			//    w.Write((byte)v.z);

			//    w.Write(v.RankImage.Rank.Length);
			//    w.Write(v.RankImage.Rank);

			//    w.Write(v.RankImage.Suit.Length);
			//    w.Write(v.RankImage.Suit);

			//    w.Write((byte)v.RankImage.IsPairableMode);
			//}
		}

		public void ReadFrom(Stream m)
		{
			this.Layout = null;


			//var r = new BinaryReader(m);


		}
	}
}
