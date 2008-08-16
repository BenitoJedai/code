using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.Shared.Lambda;
using Mahjong.Shared;

namespace Mahjong.Code
{
	[Script]
	public class MyCanvas : Canvas
	{
		public const int DefaultWidth = 480;
		public const int DefaultHeight = 640;

		[Script]
		public class VisibleTile
		{
			public readonly Canvas Control = new Canvas();

			public readonly RankAsset Value;

			public VisibleTile(Asset.Settings s, RankAsset r)
			{
				Value = r;

				Control.Width = s.OuterWidth;
				Control.Height = s.OuterHeight;

				new Image
				{
					Source = r.ResourceAlias.ToSource()
				}.AttachTo(Control).MoveTo(s.OuterWidth - s.InnerWidth - 1, 1);

				new Image
				{
					Source = s.BackgroundTile.ResourceAlias.ToSource()
				}.AttachTo(Control);

				Control.MouseEnter +=
					delegate
					{
						Control.Opacity = 0.8;
					};

				Control.MouseLeave +=
					delegate
					{
						Control.Opacity = 1;
					};
			}
		}

		public MyCanvas()
		{
			this.Width = DefaultWidth;
			this.Height = DefaultHeight;

			var stuff = Asset.Bamboo.
					Concat(Asset.Characters).
					Concat(Asset.Dots).
					Concat(Asset.Dragons).
					Concat(Asset.Flowers).
					Concat(Asset.Seasons).
					Concat(Asset.Winds);

			var random = stuff.Concat(stuff).Randomize().ToArray();

			var s = new Asset.Settings();

			const int TilesPerLine = 12;

			for (int i = 0; i < random.Length; i++)
			{
				new VisibleTile(s, random[i]).Control.AttachTo(this).MoveTo(32 + (s.InnerWidth + 4) * (TilesPerLine - (i % TilesPerLine)), 32 + (s.InnerHeight + 4) * Convert.ToInt32(i / TilesPerLine));
			}


		}
	}
}
