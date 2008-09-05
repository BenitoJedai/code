using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Shared.Avalon.Extensions;
using Mahjong.Shared;
using System.Windows.Controls;

namespace Mahjong.Code
{
	[Script]
	public partial class VisibleLayout
	{
		public readonly SettingsInfo Settings;
		public readonly AbstractAsset.Settings TileSettings;

		public readonly Canvas Container;

		public VisibleLayout(SettingsInfo s)
		{
			this.Settings = s;
			this.TileSettings = new AbstractAsset.Settings { Scale = s.Scale };

			this.Container = new Canvas { Width = s.ScaledWidth, Height = s.ScaledHeight };

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
				if (_Layout != null)
				{
					LayoutClear();
				}

				_Layout = value;

				if (_Layout != null)
				{
					// create

					LayoutUpdate();
				}
			}
		}

		private void LayoutClear()
		{
		}

	



		private void LayoutUpdate()
		{
			this.LayoutProgress = new Future();

			this.TilesInfo = new TilesInfoType(this.Layout.CountZ, this.Layout.Tiles);
			

			LayoutUpdatePairs(
				delegate
				{
					if (LayoutChanging != null)
						LayoutChanging();

					LayoutUpdateFinalize();
				}
			);

			
		}

	

		private void LayoutUpdateFinalize()
		{
			var s = this.TileSettings;
			var o = 3;
			var f = this.Tiles.OrderBy(k => k.z).Last();
			var h = f.z + o;

			if (h < o)
				h = o;

			// just render what we know
			this.Tiles.ForEach(
				(entry, index, SignalNext) =>
				{
					var tt = new VisibleTile(s, entry.RankImage) { Entry = entry };

					tt.Control.MoveTo(
						48 + ((s.ScaledInnerWidth + s.ScaledSpacing) * entry.x + (s.ScaledBorderWidth + s.ScaledSpacing) * entry.z) / 2,
						32 + ((s.ScaledInnerHeight + s.ScaledSpacing) * entry.y - (s.ScaledBorderHeight + s.ScaledSpacing) * entry.z) / 2
					).AttachTo(this.Container);



					tt.BlackFilter.Opacity = (double)(h - (entry.z + o)) / h;

					entry.Tile.Value = tt;

					1.AtDelay(SignalNext);
				}
			)(
				delegate
				{
					if (LayoutChanged != null)
						LayoutChanged();

					LayoutProgress.Signal();
				}
			);
		}

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
