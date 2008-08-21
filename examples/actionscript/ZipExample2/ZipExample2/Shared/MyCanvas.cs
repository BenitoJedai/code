using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.Shared.Archive.Extensions;
using ScriptCoreLib.Shared.Lambda;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.IO;
using ScriptCoreLib.Shared.Archive;

namespace ZipExample2.Shared
{
	[Script]
	public class MyCanvas : Canvas
	{
		public const int DefaultWidth = 480;
		public const int DefaultHeight = 320;

		public MyCanvas()
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
				Source = "assets/ZipExample2/help_idle.png".ToSource()
			}.AttachTo(this);

			var help = new Image
			{
				Source = "assets/ZipExample2/help.png".ToSource()
			}.AttachTo(this);

			help.Opacity = 0;

			var img = new Image
			{
				Source = "assets/ZipExample2/jsc.png".ToSource()
			}.MoveTo(DefaultWidth - 128, DefaultHeight - 128).AttachTo(this);

			var t = new TextBox
			{
				FontSize = 32,
				Text = "archive: ",
				BorderThickness = new Thickness(0),
				Foreground = 0xffffffff.ToSolidColorBrush(),
				Background = Brushes.Transparent,
				IsReadOnly = true
			}.MoveTo(32, 32).AttachTo(this);


			var zip = "assets/ZipExample2/dude5.zip".ToZIPFile();

			t.AppendText(zip.Items.Count + " " + zip.Items.First().FileName);

			var AnimatedImage = new Image
			{
				RenderTransform = new ScaleTransform { ScaleX = 2, ScaleY = 2 }
			}.MoveTo(64, 64).AttachTo(this);

			var AnimationIndex = 0;
			var AnimationCounter = 0;

			(1000 / 8).AtInterval(
				() => 
				{
					AnimationCounter++;

					AnimationIndex += 8;

					if ((AnimationCounter % (zip.Items.Count / 8)) == 0)
						AnimationIndex++;

					var n = zip.Items.AtModulus(AnimationIndex);

			

					t.Text = "# " + n.FileName;

					

					AnimatedImage.Source = n.Data.ToSource();
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
