using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Mahjong.Shared;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.Shared.Lambda;

namespace Mahjong.Code
{
	[Script]
	public partial class VisibleLayout
	{
		public readonly SettingsInfo Settings;
		public readonly AbstractAsset.Settings TileSettings;

		public readonly Canvas Container;

		public readonly Canvas Overlay;

		public VisibleLayout(SettingsInfo s)
		{
			this.Settings = s;
			this.TileSettings = new AbstractAsset.Settings { Scale = s.Scale };

			this.Container = new Canvas { Width = s.ScaledWidth, Height = s.ScaledHeight };

			if (s.CreateOverlay)
			{
				this.Overlay = new Canvas
				{
					Width = s.ScaledWidth,
					Height = s.ScaledHeight,
					Opacity = 0
				};

				new Rectangle
				{
					Fill = Brushes.Blue,
					Width = s.ScaledWidth,
					Height = s.ScaledHeight
				}.AttachTo(Overlay);

			}
		}

		Layout _Layout;

		public Layout Layout
		{
			get
			{
				return _Layout;
			}
			set
			{
				this.LayoutProgress.Continue(
					delegate
					{
						if (_Layout != null)
						{
							LayoutClear();


							if (LayoutDestroyed != null)
								LayoutDestroyed();
						}

						_Layout = value;

						if (_Layout != null)
						{
							// create

							LayoutUpdate();
						}
					}
				);
			}
		}

		private void LayoutClear()
		{
			var t = this.TilesInfo;

			if (t != null)
			{
				this.LayoutProgress.Continue(
					delegate
					{
						foreach (var v in t.Tiles)
						{
							v.Tile.Value.Control.Orphanize();
							v.Tile.Value.Overlay.Orphanize();
						}

						
					}
				);
			}

			this.GoBackHistory.Clear();
			this.GoForwardHistory.Clear();

			if (GoBackUnavailable != null)
				GoBackUnavailable();

			if (GoForwardUnavailable != null)
				GoForwardUnavailable();
		}





		private void LayoutUpdate()
		{
			this.LayoutProgress = new Future();

			this.TilesInfo = new TilesInfoType(this.Layout.CountZ, this.Layout.Tiles);


			LayoutUpdatePairs(
				delegate
				{
					using (var stuff = AbstractAsset.RandomizedPairs.AsCyclicEnumerable().GetEnumerator())
					{
						foreach (var p in this.Pairs)
						{
							var k = stuff.Take();

							p.Left.RankImage = k.Left;
							p.Right.RankImage = k.Right;
						}
					}

					if (LayoutChanging != null)
						LayoutChanging();

					LayoutUpdateFinalize();
				}
			);


		}



		private void LayoutUpdateFinalize()
		{
			var s = this.TileSettings;
			var o = 2;
			var h = 0;

			if (this.Tiles.Length > 0)
			{
				var f = this.Tiles.OrderBy(k => k.z).Last();
				h = f.z + o;

				if (h < o)
					h = o;
			}

			// just render what we know
			this.Tiles.ForEach(
				(entry, index, SignalNext) =>
				{
					var tt = new VisibleTile(s, entry.RankImage)
					{
						Entry = entry,
						LayoutProgress = this.LayoutProgress,
					};

					var x = 48 + ((s.ScaledInnerWidth + s.ScaledSpacing) * entry.x + (s.ScaledBorderWidth + s.ScaledSpacing) * entry.z) / 2;
					var y = 32 + ((s.ScaledInnerHeight + s.ScaledSpacing) * entry.y - (s.ScaledBorderHeight + s.ScaledSpacing) * entry.z) / 2;

					tt.Control.MoveTo(
						x,
						y
					).AttachTo(this.Container);


					if (this.Settings.CreateOverlay)
					{
						tt.Overlay = new Rectangle
						{
							Fill = Brushes.Yellow,
							Width = s.ScaledOuterWidth,
							Height = s.ScaledOuterHeight,
							Cursor = Cursors.Hand
						}.MoveTo(x, y).AttachTo(this.Overlay);
					}

					tt.BlackFilter.Opacity = (double)(h - (entry.z + o)) / h;

					entry.Tile.Value = tt;

					tt.Visible = entry.Visible;

					10.AtDelay(SignalNext);
				}
			)(
				delegate
				{
					if (LayoutChanged != null)
						LayoutChanged();

					1.AtDelay(LayoutProgress.Signal);
				}
			);
		}

		public event Action LayoutDestroyed;
		public event Action LayoutChanging;
		public event Action LayoutChanged;


		/// <summary>
		/// null when no layout given,
		/// wait state when layout loaded
		/// direct dispatch when loaded
		/// </summary>
		public Future LayoutProgress;

	}
}
