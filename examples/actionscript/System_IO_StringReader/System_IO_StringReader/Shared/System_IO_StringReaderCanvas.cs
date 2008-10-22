﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using ScriptCoreLib.Shared.Lambda;
using System.IO;

namespace System_IO_StringReader.Shared
{
	[Script]
	public class System_IO_StringReaderCanvas : Canvas
	{
		public const int DefaultWidth = 480;
		public const int DefaultHeight = 320;

		public System_IO_StringReaderCanvas()
		{
			Width = DefaultWidth;
			Height = DefaultHeight;

			#region Gradient
			for (int i = 0; i < DefaultHeight; i += 4)
			{
				new Rectangle
				{
					Fill = ((uint)(0xff00007F + Convert.ToInt32(128 * i / DefaultHeight))).ToSolidColorBrush(),
					Width = DefaultWidth,
					Height = 4,
				}.MoveTo(0, i).AttachTo(this);
			}
			#endregion

			var help_idle = new Image
			{
				Source = "assets/System_IO_StringReader/help_idle.png".ToSource()
			}.AttachTo(this);

			var help = new Image
			{
				Source = "assets/System_IO_StringReader/help.png".ToSource()
			}.AttachTo(this);

			help.Opacity = 0;

			var img = new Image
			{
				Source = "assets/System_IO_StringReader/jsc.png".ToSource()
			}.MoveTo(DefaultWidth - 128, DefaultHeight - 128).AttachTo(this);

			var t = new TextBox
			{
				FontSize = 32,
				Text = "powered by jsc",
				BorderThickness = new Thickness(0),
				Foreground = 0xffffffff.ToSolidColorBrush(),
				Background = Brushes.Transparent,
				IsReadOnly = true
			}.MoveTo(32, 32).AttachTo(this);

			var Data = new Future<string>();

			"assets/System_IO_StringReader/data.txt".ToStringAsset(Data);

			Data.Continue(
				value =>
				{
					using (var s = new StringReader(value))
					{
						
						// skip header
						var header = s.ReadLine();

						var _1 = s.ReadLine();
						var _2 = s.ReadLine();

						Console.WriteLine("" + new { _1, _1.Length });
						Console.WriteLine("" + new { _2, _2.Length });

						t.Text = "Content: " + _2;

						var footer = s.ReadLine();

						var empty = s.ReadLine();
					}
				}
			);

			help_idle.Opacity = 0;
			help.Opacity = 1;
			img.Opacity = 0.5;

			t.MouseEnter +=
				delegate
				{
					help_idle.Opacity = 1;
					help.Opacity = 0;

					img.Opacity = 1;
					t.Foreground = 0xffffff00.ToSolidColorBrush();
				};

			t.MouseLeave +=
				delegate
				{
					help_idle.Opacity = 0;
					help.Opacity = 1;

					img.Opacity = 0.5;
					t.Foreground = 0xffffffff.ToSolidColorBrush();
				};



		}
	}
}
