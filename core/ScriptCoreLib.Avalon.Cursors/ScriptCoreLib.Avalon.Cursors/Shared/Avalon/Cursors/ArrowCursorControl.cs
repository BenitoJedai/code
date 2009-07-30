using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Media;

namespace ScriptCoreLib.Shared.Avalon.Cursors
{
	[Script]
	public class ArrowCursorControl : ISupportsContainer
	{
		public Canvas Container { get; set; }

		public readonly Image Background;

		public readonly Image Red;
		public readonly Image Green;
		public readonly Image Blue;

		public readonly Image Cyan;
		public readonly Image Magenta;
		public readonly Image Yellow;

		public const int Size = 32;

		public ArrowCursorControl()
		{
			this.Container = new Canvas
			{
				Width = Size,
				Height = Size
			};

			Func<string, Image> f =
				e =>
					new Image
					{
						Source = (Assets.Path + e).ToSource(),
						Width = Size,
						Height = Size,
						Opacity = 0
					}.AttachTo(this.Container);

			this.Background = f("/Aero_Arrow.png");
			this.Background.Opacity = 1;

			this.Cyan = f("/Aero_Arrow_cyan.png");
			this.Magenta = f("/Aero_Arrow_magenta.png");
			this.Yellow = f("/Aero_Arrow_yellow.png");

			this.Red = f("/Aero_Arrow_red.png");
			this.Green = f("/Aero_Arrow_green.png");
			this.Blue = f("/Aero_Arrow_blue.png");

		}

		public static double GetColorDistance(Color x, Color y)
		{
			var r = (int)x.R - y.R;
			var g = (int)x.G - y.G;
			var b = (int)x.B - y.B;

			return Math.Sqrt(b * b + g * g + b * b);
		}

		public double DefaultColorOpacity = 0.8;

		public Color Color
		{
			set
			{
				// should implement this:
				// http://en.wikipedia.org/wiki/Image:HSV-RGB-comparison.svg

				var a = Enumerable.ToArray(
					from k in new[]
					{
						new { c = Colors.White, i = (Image)null }, 
						new { c = Colors.Red, i = this.Red }, 
						new { c = Colors.Green, i = this.Green }, 
						new { c = Colors.Blue, i = this.Blue }, 
						new { c = Colors.Cyan, i = this.Cyan }, 
						new { c = Colors.Magenta, i = this.Magenta }, 
						new { c = Colors.Yellow, i = this.Yellow } 
					}
					let z = GetColorDistance(k.c, value)
					orderby z
					select new { z, k.i }
				);

				if (a[0].i != null)
					a[0].i.Opacity = DefaultColorOpacity;

				for (int i = 1; i < a.Length; i++)
				{
					if (a[i].i != null)
						a[i].i.Opacity = 0;
				}
			}
		}

	}
}
