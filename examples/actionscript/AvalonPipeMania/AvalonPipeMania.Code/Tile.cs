using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using AvalonPipeMania.Assets.Shared;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Input;
using System.Windows.Media;

namespace AvalonPipeMania.Code
{
	[Script]
	public class Tile
	{
		private static Random rnd = new Random();

		public readonly Canvas Container;

		public const int ShadowBorder = 8;
		public const int Size = 64;

		public readonly Rectangle Overlay;

		public readonly Image YellowFilter;
		
		public readonly Image Shadow;

		public double MaxBlackFilterOpacity = 0.7;

		public Tile()
		{
			this.Container = new Canvas
			{
				Width = Size,
				Height = Size
			};

			this.Shadow = new Image
			{
				Source = (KnownAssets.Path.Data + "/tile0_black_unfocus8.png").ToSource(),
			};

			new Image
			{
				Source = (KnownAssets.Path.Data + "/tile0.png").ToSource(),
			}.AttachTo(this.Container);



			var BlackFilter = new Image
			{
				Source = (KnownAssets.Path.Data + "/tile0_black.png").ToSource(),
				Opacity = rnd.NextDouble() * MaxBlackFilterOpacity
			}.AttachTo(this.Container);

			this.YellowFilter = new Image
			{
				Source = (KnownAssets.Path.Data + "/tile0_yellow.png").ToSource(),
				Opacity = 0.4,
				Visibility = System.Windows.Visibility.Hidden,
			}.AttachTo(this.Container);


			this.Overlay = new Rectangle
			{
				Cursor = Cursors.Hand,
				Fill = Brushes.Black,
				Width = Size,
				Height = 52,
				Opacity = 0
			}.MoveTo(ShadowBorder, ShadowBorder);
		}
	}
}
