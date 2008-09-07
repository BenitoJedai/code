using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.Shared.Lambda;
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
				Text = "we",

			}.MoveTo(32, 32).AttachTo(this);

			Func<string, IEnumerable<string>> Interpolate =
				k => Enumerable.Range(1, 2).Select(i => k + " " + i).ConcatSingle(k);

			var Data =
	@"
( )	 Action
( )	 Adventure
( )	 Animation
( )	 Biography
( )	 Comedy
( )	 Crime
( )	 Documentary
( )	 Drama
( )	 Family
( )	 Fantasy
( )	 Film-Noir
( )	 History
( )	 Horror
( )	 Independent
( )	 Music
( )	 Musical
( )	 Mystery
( )	 Romance
( )	 Sci-Fi
( )	 Short
( )	 Sport
( )	 Thriller
( )	 TV mini-series
( )	 War
( )	 Western
	".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).SelectMany(Interpolate);

		


			var Anwsers = new TextBox
			{
				BorderThickness = new Thickness(0),
				AcceptsReturn = true,
				Width = 200,
				Height = 200
			}.MoveTo(32, 64).AttachTo(this);


			int Counter = 0;

			Action UpdateSummary =
				delegate
				{
					var _c = Anwsers.Text.Length;
					var _lf = Anwsers.Text.Count(Environment.NewLine);
					var _n = Anwsers.Text.Count("\n");
					var _r = Anwsers.Text.Count("\r");

					Anwsers.AppendTextLine("linefeeds: " + _lf);
					Anwsers.AppendTextLine("n: " + _n);
					Anwsers.AppendTextLine("r: " + _r);
					Anwsers.AppendTextLine("chars: " + _c);
				};

			Action Update =
				delegate
				{
					var DataSelected = Data.Where(k => k.ToLower().Contains(t.Text.ToLower())).Select(k => k.Length + " [" + k + "]").ToArray();


					Anwsers.Text = "";
					Counter++;

					Anwsers.AppendTextLine("Counter: " + Counter);
					Anwsers.AppendTextLine("Count: " + DataSelected.Length);
					Anwsers.AppendTextLine(DataSelected);

					UpdateSummary();
				};

			Anwsers.AppendTextLine();

			UpdateSummary();

			t.KeyUp +=
				(sender, ev) =>
				{
					if (ev.Key == Key.Down)
						Anwsers.Text = "down";
					else if (ev.Key == Key.Up)
						Anwsers.Text = "up";
					else
					{
						Update();
					}

				};

			2.AtDelay(UpdateSummary);


		}
	}
}
