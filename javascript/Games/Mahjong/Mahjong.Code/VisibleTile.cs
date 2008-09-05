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

		public readonly Image RankImage;

		public readonly Image BlackFilter;
		public readonly Image YellowFilter;
		public readonly Image GreenFilter;

		public VisibleLayout.Entry Entry;




		public readonly AbstractAsset.Settings Settings;

		public VisibleTile(AbstractAsset.Settings s, RankAsset r)
		{
			this.Settings = s;


			Control.Width = s.OuterWidth;
			Control.Height = s.OuterHeight;

			new Image
			{
				Source = s.BackgroundTileShadow.ResourceAlias.ToSource(),
				Stretch = System.Windows.Media.Stretch.Fill,
				Width = s.ScaledShadowWidth,
				Height = s.ScaledShadowHeight
			}.AttachTo(Control).MoveTo(-4 * s.Scale, -4 * s.Scale);

			new Image
			{
				Source = s.BackgroundTile.ResourceAlias.ToSource(),
				Stretch = System.Windows.Media.Stretch.Fill,
				Width = s.ScaledOuterWidth,
				Height = s.ScaledOuterHeight
			}.AttachTo(Control);


			RankImage = new Image
			{
				Source = r.ResourceAlias.ToSource(),
				Stretch = System.Windows.Media.Stretch.Fill,
				Width = s.ScaledInnerWidth,
				Height = s.ScaledInnerHeight
			}.AttachTo(Control).MoveTo((s.ScaledOuterWidth - s.ScaledInnerWidth - 1), 1 * s.Scale);



			BlackFilter = new Image
			{
				Source = s.BackgroundTileBlack.ResourceAlias.ToSource(),
				Stretch = System.Windows.Media.Stretch.Fill,
				Width = s.ScaledOuterWidth,
				Height = s.ScaledOuterHeight
			}.AttachTo(Control);

			YellowFilter = new Image
			{
				Source = s.BackgroundTileYellow.ResourceAlias.ToSource(),
				Stretch = System.Windows.Media.Stretch.Fill,
				Width = s.ScaledShadowWidth,
				Height = s.ScaledShadowHeight,
				Opacity = 0
			}.AttachTo(Control).MoveTo(-4 * s.Scale, -4 * s.Scale);


			GreenFilter = new Image
			{
				Source = s.BackgroundTileGreen.ResourceAlias.ToSource(),
				Stretch = System.Windows.Media.Stretch.Fill,
				Width = s.ScaledShadowWidth,
				Height = s.ScaledShadowHeight,
				Opacity = 0
			}.AttachTo(Control).MoveTo(-4 * s.Scale, -4 * s.Scale);


		}
	}

}
