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
Action
Adventure
Animation
Biography
Comedy
Crime
Documentary
Drama
Family
Fantasy
Film-Noir
History
Horror
Independent
Music
Musical
Mystery
Romance
Sci-Fi
Short
Sport
Thriller
TV mini-series
War
Western
	".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(k => k.Trim()).WhereNot(string.IsNullOrEmpty).SelectMany(Interpolate);

		


			var Anwsers = new TextBox
			{
				BorderThickness = new Thickness(0),
				AcceptsReturn = true,
				Width = 200,
				Height = 200
			}.MoveTo(32, 64).AttachTo(this);



			Action Update =
				delegate
				{
					var Filter = t.Text.ToLower();
					var DataSelected = from k in Data
									   where k.ToLower().Contains(Filter)
									   orderby k select k;


					
					Anwsers.Text = DataSelected.ConcatToLines();
				};

			Update();


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



		}
	}
}
