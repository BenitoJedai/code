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

			new TextBox
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

			var arrow = new Image
			{
				Source = "assets/BrowserAvalonExample.Assets/arrow.png".ToSource()
			}.AttachTo(this).MoveTo(4, 5);

			var overlay = new Rectangle
			{
				Fill = Brushes.Red,
				Width = DefaultWidth,
				Height = DefaultHeight,
				Opacity = 0
			}.AttachTo(this);


			overlay.MouseMove +=
				(s, a)=>
				{
					var p = a.GetPosition(this);

					arrow.MoveTo(p.X, p.Y);
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
