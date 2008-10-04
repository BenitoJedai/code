using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Controls;

namespace AvalonPipeMania.Code
{
	[Script]
	public class Field
	{
		public readonly Canvas Shadow;
		public readonly Canvas Content;
		public readonly Canvas Overlay;
		public readonly Canvas Container;

		public readonly int Width;
		public readonly int Height;

		public Field(int width, int height)
		{
			this.Width = Tile.Size * width + Tile.ShadowBorder * 2;
			this.Height = Tile.Size * height + Tile.ShadowBorder * 2;

			this.Shadow = new Canvas
			{
				Width = this.Width,
				Height = this.Height,
			};

			this.Content = new Canvas
			{
				Width = this.Width,
				Height = this.Height,
			};

			this.Overlay = new Canvas
			{
				Width = this.Width,
				Height = this.Height,
			};

			this.Container = new Canvas
			{
				Width = this.Width,
				Height = this.Height,
			};

			this.Shadow.AttachTo(this.Container);
			this.Content.AttachTo(this.Container);

			for (int ix = 0; ix < width; ix++)
				for (int iy = 0; iy < height; iy++)
				{
					var tile = new Tile();

					tile.Shadow.MoveTo(64 * ix, 52 * iy).AttachTo(this.Shadow);
					tile.Container.MoveTo(64 * ix +  Tile.ShadowBorder, 52 * iy+  Tile.ShadowBorder).AttachTo(this.Content);
					tile.Overlay.MoveTo(64 * ix +  Tile.ShadowBorder, 52 * iy+  Tile.ShadowBorder).AttachTo(this.Overlay);

					tile.Overlay.MouseEnter +=
						delegate
						{
							tile.YellowFilter.Visibility = System.Windows.Visibility.Visible;
						};

					tile.Overlay.MouseLeave +=
						delegate
						{
							tile.YellowFilter.Visibility = System.Windows.Visibility.Hidden;
						};
				}

		}
	}
}
