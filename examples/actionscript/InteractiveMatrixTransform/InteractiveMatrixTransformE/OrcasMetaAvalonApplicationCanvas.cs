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
using InteractiveMatrixTransformE.AffineEngine;

namespace InteractiveMatrixTransformE
{
	public class OrcasMetaAvalonApplicationCanvas : Canvas
	{
		// http://code.google.com/p/kml-library/source/browse/#svn/trunk/KMLib/Abstract

		public const int DefaultWidth = 800;
		public const int DefaultHeight = 500;

		public OrcasMetaAvalonApplicationCanvas()
		{
			Width = DefaultWidth;
			Height = DefaultHeight;

			this.ClipToBounds = true;

			Colors.Blue.ToGradient(Colors.Red, DefaultHeight / 4).Select(
				(c, i) =>
					new Rectangle
					{
						Fill = new SolidColorBrush(c),
						Width = DefaultWidth,
						Height = 4,
					}.MoveTo(0, i * 4).AttachTo(this)
			).ToArray();



			//var help = new Image
			//{
			//    Source = (KnownAssets.Path.Assets + "/help.png").ToSource()
			//}.AttachTo(this);

			//help.Opacity = 0;

			var img = new Image
			{
				Source = ("assets/InteractiveMatrixTransformE/jsc.png").ToSource()
			}.MoveTo(DefaultWidth - 128, DefaultHeight - 128).AttachTo(this);

			var t = new TextBox
			{
				FontSize = 10,
				Text = "powered by jsc",
				BorderThickness = new Thickness(0),
				Foreground = 0xffffffff.ToSolidColorBrush(),
				Background = Brushes.Transparent,
				IsReadOnly = true,
				Width = DefaultWidth
			}.MoveTo(8, 8).AttachTo(this);



			//help.Opacity = 1;
			img.Opacity = 0.5;








			// cursor position calculations are not ready
			// for transofrmed elements.
			// we will provide a floor for those events...
			var shadow = new Rectangle
			{
				Width = DefaultWidth,
				Height = DefaultHeight,

				Fill = Brushes.Black,
			}.AttachTo(this);

			var shadowa = shadow.ToAnimatedOpacity();

			shadowa.Opacity = 0;



			var a = new AffineMesh();

			Func<double, double, double, Brush, AffinePoint> a_Add =
				(X, Y, Z, Fill) =>
				{
					var p = new AffinePoint { Z = Z, X = X, Y = Y };
					var h = new Rectangle { Fill = Fill, Width = 4, Height = 4 }.AttachTo(this);
					//var ht = new TextBox { }.AttachTo(this);

					var historysize = 64;
					var history = new Queue<Rectangle>();

					Action<AffinePoint> p_Update =
						pp =>
						{
							if (history.Count > historysize)
							{
								history.Dequeue();
							}

							// -100 == 0.5
							// 0 == 1
							// +100 == 2

							//var zoom = (pp.Z) / 1000;

							var zoom = 0.5;

							var pp_X = DefaultWidth / 2 - 4 + pp.X * zoom;
							var pp_Y = DefaultHeight / 2 - 4 + pp.Y * zoom;
							// Z is ignored here
							// but could be used for sorting

							h.MoveTo(pp_X, pp_Y);

							//var hh = new Rectangle { Fill = Fill, Width = 4, Height = 4, Opacity = 1 }.MoveTo(pp_X, pp_Y).AttachTo(this);
							//history.Enqueue(hh);

							//foreach (var k in history.Select((c, i) => new { c, i }))
							//{
							//    k.c.Opacity = ((double)k.i / historysize) * 0.6;
							//}

							//ht.MoveTo(
							//    DefaultWidth / 2 + pp.X - 4,
							//    DefaultHeight / 2 + pp.Y + 4
							//);

							//ht.Text = new { pp.X, pp.Y, pp.Z }.ToString();
						};

					p.Tag = p_Update;

					a.Add(p);

					p_Update(p);

					return p;
				};


			Enumerable.Range(0, 10).Select(X => a_Add(X * 10, 0, 0, Brushes.Red)).ToArray();
			Enumerable.Range(0, 10).Select(Y => a_Add(0, Y * 10, 0, Brushes.Blue)).ToArray();
			Enumerable.Range(0, 10).Select(Z => a_Add(0, 0, Z * 10, Brushes.GreenYellow)).ToArray();

			Enumerable.Range(0, 10).Select(X => a_Add(X * 10 + 100, 0, 0, Brushes.Red)).ToArray();
			Enumerable.Range(0, 10).Select(Y => a_Add(100, Y * 10, 0, Brushes.Blue)).ToArray();
			Enumerable.Range(0, 10).Select(Z => a_Add(100, 0, Z * 10, Brushes.GreenYellow)).ToArray();


			Enumerable.Range(0, 100).Select(X => a_Add(X * 10 + 200, 0, 0, Brushes.Red)).ToArray();
			Enumerable.Range(0, 10).Select(Y => a_Add(200, Y * 10, 0, Brushes.Blue)).ToArray();
			Enumerable.Range(0, 10).Select(Z => a_Add(200, 0, Z * 10, Brushes.GreenYellow)).ToArray();

			{
				var radius = 100;
				foreach (var i in Enumerable.Range(0, 90).Select(aa => (aa * 4).DegreesToRadians()))
				{
					a_Add(Math.Cos(i) * radius, Math.Sin(i) * radius, 0, Brushes.GreenYellow);
					a_Add(Math.Cos(i) * radius, 0, Math.Sin(i) * radius, Brushes.BlueViolet);
					a_Add(0, Math.Cos(i) * radius, Math.Sin(i) * radius, Brushes.Magenta);
				}
			}

			{
				var radius = 200;
				foreach (var i in Enumerable.Range(0, 90).Select(aa => (aa * 4).DegreesToRadians()))
				{
					a_Add(Math.Cos(i) * radius, Math.Sin(i) * radius, 0, Brushes.GreenYellow);
					a_Add(Math.Cos(i) * radius, 0, Math.Sin(i) * radius, Brushes.BlueViolet);
					a_Add(0, Math.Cos(i) * radius, Math.Sin(i) * radius, Brushes.Magenta);
				}
			}

			{
				var radius = 500;
				foreach (var i in Enumerable.Range(0, 90).Select(aa => (aa * 4).DegreesToRadians()))
				{
					a_Add(Math.Cos(i) * radius, Math.Sin(i) * radius, 0, Brushes.GreenYellow);
					a_Add(Math.Cos(i) * radius, 0, Math.Sin(i) * radius, Brushes.BlueViolet);
					a_Add(0, Math.Cos(i) * radius, Math.Sin(i) * radius, Brushes.Magenta);
				}
			}

			a_Add(200, 0, 0, Brushes.BlueViolet);
			a_Add(0, 200, 0, Brushes.Yellow);
			a_Add(0, 0, 200, Brushes.Red);

			(1000 / 50).AtIntervalWithCounter(
				c =>
				{
					// rotate floor
					var _a = a.ToRotation(
						new AffineRotation
						{
							XY = 0.01,
							YZ = 0.02,
							XZ = 0.03
						}
					);

					//var _a = a.ToRotation(0, 0.01);

					a = _a;

					//var _a = a.ToRotation(c * 0.01, c * 0.005);
					//var _a = a.ToRotation(c * 0.01, 0);

					foreach (var p in _a.Points)
					{
						((Action<AffinePoint>)p.Tag)(p);
					}
				}
			);

		}


	}
}
