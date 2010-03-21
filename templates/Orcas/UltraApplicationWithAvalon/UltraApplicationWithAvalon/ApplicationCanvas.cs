using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using ScriptCoreLib.Avalon;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.Extensions;
using System.Windows;

namespace UltraApplicationWithAvalon
{
	public class ApplicationCanvas : Canvas
	{
		// This type runs in
		// - .net
		// - javascript
		// - flash

		public IUltraWebService WebService { get; set; }

		public ApplicationCanvas()
		{
			this.Width = 400;
			this.Height = 400;
			this.Background = Brushes.White;


			var c = new Canvas().AttachTo(this);

			var t = new TextBox
			{
				AcceptsReturn = true,
				BorderThickness = new Thickness(0)
			};

			t.AppendTextLine("Select 3 points to create new image.");
			t.AppendTextLine("1. TopRight");
			t.AppendTextLine("2. BottomLeft");
			t.AppendTextLine("3. TopLeft");

			t.SizeTo(400, 400).AttachTo(c);

			{
				var logo = new Avalon.Images.jsc();

				logo.Opacity = 0.5;
				logo.AttachTo(this);
				logo.MoveTo(100, 100);

				var r = new Rectangle
				{
					Width = 400,
					Height = 400,
					Fill = Brushes.White,
					Opacity = 0,
					Cursor = Cursors.Hand
				};

				r.AttachTo(this);
				r.MoveTo(0, 0);

				var a = new { X = default(double), Y = default(double) }.ToEmptyList();


				r.MouseLeftButtonUp +=
					(s, e) =>
					{
						var p = e.GetPosition(r);
						var pp = new { p.X, p.Y };

						a.Add(pp);


						this.WebService.GetTime(pp.ToString() + " : ",
							y =>
							{
								t.AppendTextLine(y);
							}
						);

						if (a.Count == 3)
						{
							var aa = new AffineTransform();

							aa.Width = Avalon.Images.jsc.ImageDefaultWidth;
							aa.Height = Avalon.Images.jsc.ImageDefaultHeight;

							aa.X1 = a[0].X;
							aa.Y1 = a[0].Y;

							aa.X2 = a[1].X;
							aa.Y2 = a[1].Y;

							aa.X3 = a[2].X;
							aa.Y3 = a[2].Y;

							a.Clear();

							var jj = new Avalon.Images.jsc();

							var jja = jj.ToAnimatedOpacity();

							jja.Opacity = 0;

							jj.AttachTo(c);

							jj.RenderTransform = aa;


							jja.Opacity = 1;

							5000.AtDelay(
								delegate
								{
									jja.Opacity = 0;
								}
							);
						}
					};
			}
		}


	}
}
