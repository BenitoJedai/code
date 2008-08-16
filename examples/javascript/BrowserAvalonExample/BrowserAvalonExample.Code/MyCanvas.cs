using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;

namespace BrowserAvalonExample.Code
{
	[Script]
	public class MyCanvas : Canvas
	{
		public const int DefaultWidth = 480;
		public const int DefaultHeight = 320;

		public MyCanvas()
		{
			// jsc:javascript does not work well with structs

			this.Width = DefaultWidth;
			this.Height = DefaultHeight;

			new Rectangle
			{
				Fill = Brushes.Red,
				Width = DefaultWidth,
				Height = DefaultHeight / 2
			}.AttachTo(this).MoveTo(0, 0);

			new Rectangle
			{
				Fill = Brushes.Yellow,
				Width = DefaultWidth,
				Height = DefaultHeight / 2
			}.AttachTo(this).MoveTo(0, DefaultHeight / 2);


			new Rectangle
			{
				Fill = Brushes.GreenYellow,
				Width = 62,
				Height = 62
			}.AttachTo(this).MoveTo(32, 8);

			var info = new TextBox
			{
				Text = "hello world",
				Background = Brushes.Transparent,
				BorderThickness = new Thickness(0),
				IsReadOnly = true
			}.AttachTo(this).MoveTo(32, 32);

			var e = new Image 
			{
				Source = "assets/BrowserAvalonExample.Assets/tipsi2.png".ToSource()
			}.AttachTo(this).MoveTo(4, 5);

			var underlay = new Canvas
			{
				Width = DefaultWidth,
				Height = DefaultHeight,

			}.AttachTo(this);

			var arrow = new Image
			{
				Source = "assets/BrowserAvalonExample.Assets/arrow.png".ToSource()
			}.AttachTo(this).MoveTo(4, 5);

			var bluearrow = new Image
			{
				Source = "assets/BrowserAvalonExample.Assets/bluearrow.png".ToSource()
			}.AttachTo(this).MoveTo(4, 5);


			bluearrow.Visibility = Visibility.Hidden;



			Action<double, double> DrawBrush =
				(x, y) =>
				{
					new Image
					{
						Source = "assets/BrowserAvalonExample.Assets/bluebrush.png".ToSource()
					}.AttachTo(underlay).MoveTo(x, y);
				};

			var overlay = new Rectangle
			{
				Fill = Brushes.Red,
				Width = DefaultWidth,
				Height = DefaultHeight,
				Opacity = 0
			}.AttachTo(this).MoveTo(0, 0);

			var Cursor = new Point();
			var Counter = 0;
			var MyBrushPainter = 50.AtInterval(
				delegate
				{
					Counter++;

					info.Text = "counter: " + Counter;


					DrawBrush(Cursor.X - 32, Cursor.Y -32);

				}
			);

			MyBrushPainter.Stop();

			overlay.MouseLeftButtonDown +=
				delegate
				{
					bluearrow.Visibility = Visibility.Visible;
					MyBrushPainter.Start();
				};

			overlay.MouseLeftButtonUp +=
				delegate
				{
					bluearrow.Visibility = Visibility.Hidden;
					MyBrushPainter.Stop();
				};


			overlay.MouseMove +=
				(s, a)=>
				{
					Cursor = a.GetPosition(this);

					arrow.MoveTo(Cursor.X, Cursor.Y);
					bluearrow.MoveTo(Cursor.X, Cursor.Y);
				};

			new TextBox
			{
				Text = "enter text here",
				Background = Brushes.GreenYellow,
				Foreground = Brushes.Red,
				BorderThickness = new Thickness(0),
				IsReadOnly = false
			}.AttachTo(this).MoveTo(32, 64);

			
		}

	}
}
