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

			w.Write(this.GoBackHistory.Count);
			foreach (var v in this.GoBackHistory.AsEnumerable())
			{
				w.Write((short)v.Left.Entry.index);
				w.Write((short)v.Right.Entry.index);
			}


			w.Write(this.GoForwardHistory.Count);
			foreach (var v in this.GoForwardHistory.AsEnumerable())
			{
				w.Write((short)v.Left.Entry.index);
				w.Write((short)v.Right.Entry.index);
			}

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

			this.LayoutProgress = new Future();

			this.TilesInfo = new TilesInfoType(layout.CountZ, layout.Tiles);

			Enumerable.Range(0, r.ReadInt32()).ForEach(
				k =>
				{
					var Left = r.ReadInt16();
					var Right = r.ReadInt16();

					this.LayoutProgress.Continue(
						delegate
						{
							this.GoBackHistory.Push(
								new RemovedTilePair
								{
									Left = this.Tiles[Left].Tile.Value,
									Right = this.Tiles[Right].Tile.Value,
								}
							);
						}
					);
				}
			);


			Enumerable.Range(0, r.ReadInt32()).ForEach(
					k =>
					{
						var Left = r.ReadInt16();
						var Right = r.ReadInt16();

						this.LayoutProgress.Continue(
							delegate
							{
								this.GoForwardHistory.Push(
									new RemovedTilePair
									{
										Left = this.Tiles[Left].Tile.Value,
										Right = this.Tiles[Right].Tile.Value,
									}
								);
							}
						);
					}
				);

			this._Layout = layout;

		

			if (LayoutChanging != null)
				LayoutChanging();




			LayoutUpdateFinalize();
		}
	}
}
