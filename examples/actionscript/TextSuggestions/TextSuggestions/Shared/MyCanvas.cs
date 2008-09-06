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
using System.Windows.Input;

namespace TextSuggestions.Shared
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

			var t = new TextBox
			{
				Width = 200,
				Height = 22,
				Text = "powered by jsc",

			}.MoveTo(32, 32).AttachTo(this);



			var Anwsers = new TextBox
			{
				Text = "?",
				BorderThickness = new Thickness(0),
				AcceptsReturn = true,
				Width = 200,
				Height = 200
			}.MoveTo(32, 64).AttachTo(this);




			t.KeyUp +=
				(sender, ev) =>
				{
					if (ev.Key == Key.Down)
						Anwsers.Text = "down";
					else if (ev.Key == Key.Up)
						Anwsers.Text = "up";
					else
						Anwsers.Text = "" + new { ev.Key };
				};



		}
	}
}
