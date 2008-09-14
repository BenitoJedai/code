using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using ScriptCoreLib.Shared.Avalon.Cursors;
using System.Windows.Input;

namespace DynamicCursor.Shared
{
	[Script]
	public class DynamicCursorCanvas : Canvas
	{
		public const int DefaultWidth = 480;
		public const int DefaultHeight = 320;

		public DynamicCursorCanvas()
		{
			Width = DefaultWidth;
			Height = DefaultHeight;

			var g = Colors.Yellow.ToGradient(Colors.Red, DefaultHeight).ToArray();
			
			g.Select(
				(c, i) =>
					new Rectangle
					{
						Fill = new SolidColorBrush(c),
						Width = DefaultWidth,
						Height = 2,
					}.MoveTo(0, i).AttachTo(this)
			).ToArray();


			var cur = new ArrowCursorControl();

			cur.Cyan.Opacity = 0;
			cur.Magenta.Opacity = 0;
			cur.Yellow.Opacity = 0;


			new Rectangle
		   {
			   Fill = Brushes.Red,
			   Width = 32,
			   Height = 32
		   }.MoveTo(32, 32).AttachTo(this);

			new Rectangle
		   {
			   Fill = Brushes.Green,
			   Width = 32,
			   Height = 32
		   }.MoveTo(32 * 2, 32).AttachTo(this);

			new Rectangle
			{
				Fill = Brushes.Blue,
				Width = 32,
				Height = 32
			}.MoveTo(32 * 3, 32).AttachTo(this);


			cur.Container.AttachTo(this).MoveTo(64, 64);

			var Overlay = new Rectangle
			{
				Fill = Brushes.White,
				Width = DefaultWidth,
				Height = DefaultHeight,
				Opacity = 0
			}.AttachTo(this);


			var Overlay_Red = new Rectangle
			{
				Fill = Brushes.Red,
				Width = 32,
				Height = 32,
				Opacity = 0
			}.MoveTo(32, 32).AttachTo(this);

			var Overlay_Green = new Rectangle
			{
				Fill = Brushes.Green,
				Width = 32,
				Height = 32,
				Opacity = 0
			}.MoveTo(32 * 2, 32).AttachTo(this);

			var Overlay_Blue = new Rectangle
			{
				Fill = Brushes.Blue,
				Width = 32,
				Height = 32,
				Opacity = 0
			}.MoveTo(32 * 3, 32).AttachTo(this);


			var Overlay_Y = new Rectangle
			{
				Fill = Brushes.Blue,
				Width = 32,
				Height = DefaultHeight,
				Opacity = 0
			}.AttachTo(this);

			Overlay_Y.MouseMove +=
			(sender, e) =>
				{
					var p = e.GetPosition(this);

					cur.Color = g[Convert.ToInt32(p.Y)];
				};

			this.Cursor = Cursors.None;


			Overlay_Red.MouseEnter +=
				delegate
				{
					// http://acept.asu.edu/PiN/rdg/color/composition.shtml
					cur.Color = Colors.Red;

				};

			Overlay_Green.MouseEnter +=
				delegate
				{
					// http://acept.asu.edu/PiN/rdg/color/composition.shtml
					cur.Color = Colors.Green;
				};

			Overlay_Blue.MouseEnter +=
				delegate
				{
					// http://acept.asu.edu/PiN/rdg/color/composition.shtml
					cur.Color = Colors.Blue;
				};

			this.MouseMove +=
				(sender, e) =>
				{
					var p = e.GetPosition(this);


					cur.Container.MoveTo(p.X, p.Y);
				};
		}
	}
}
