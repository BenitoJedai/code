using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Controls;
using Mahjong.Shared;

namespace Mahjong.Code
{
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
				Source = s.BackgroundTile.ResourceAlias.ToSource(),
				Stretch = System.Windows.Media.Stretch.Fill,
				Width = s.ScaledOuterWidth,
				Height = s.ScaledOuterHeight
			}.AttachTo(Control);

			new Image
			{
				Source = r.ResourceAlias.ToSource(),
				Stretch = System.Windows.Media.Stretch.Fill,
				Width = s.ScaledInnerWidth,
				Height = s.ScaledInnerHeight 
			}.AttachTo(Control).MoveTo((s.ScaledOuterWidth - s.ScaledInnerWidth - 1) , 1 * s.Scale);


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

}
