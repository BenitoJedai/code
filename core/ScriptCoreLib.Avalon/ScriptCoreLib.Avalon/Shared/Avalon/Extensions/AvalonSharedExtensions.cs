﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Media;
using System.Windows.Controls.Primitives;
using ScriptCoreLib.Shared.Lambda;

namespace ScriptCoreLib.Shared.Avalon.Extensions
{
	[Script]
	public static class AvalonSharedExtensions
	{
		public static void ClipTo(this UIElement e, Rect r)
		{
			var c = new RectangleGeometry
			{
				Rect = r
			};

			e.Clip = c;
		}

		public static void ClipTo(this UIElement e, int x, int y, int width, int height)
		{
			e.ClipTo(
				new Rect
				{
					X = x,
					Y = y,
					Width = width,
					Height = height
				}
			);

		}

		public static void Show(this UIElement e)
		{
			if (e == null)
				return;

			e.Visibility = Visibility.Visible;
		}

		public static void Hide(this UIElement e)
		{
			if (e == null)
				return;


			e.Visibility = Visibility.Hidden;
		}


		public static void ToggleVisible(this UIElement e)
		{
			if (e.Visibility == Visibility.Visible)
				e.Hide();
			else
				e.Show();

		}

		public static IEnumerable<Brush> ToGradient(this Brush from, Brush to, int count)
		{
			var _from = from as SolidColorBrush;
			var _to = to as SolidColorBrush;

			if (_from == null)
				throw new NotSupportedException();

			if (_to == null)
				throw new NotSupportedException();

			return _from.Color.ToGradient(_to.Color, count).Select(k => (Brush)new SolidColorBrush(k));
		}

		public static IEnumerable<Color> ToGradient(this Color from, Color to, int count)
		{
			return Enumerable.Range(0, count).Select(
				i =>
				{
					var j = count - (i + 1);

					var r = Convert.ToByte((from.R * j + to.R * i) / count);
					var g = Convert.ToByte((from.G * j + to.G * i) / count);
					var b = Convert.ToByte((from.B * j + to.B * i) / count);

					return Color.FromRgb(r, g, b);
				}
			);
		}

		public static void AppendTextLine(this TextBoxBase e, string textData)
		{
			e.AppendText(textData + Environment.NewLine);
		}

		public static void AppendTextLine(this TextBoxBase e)
		{
			e.AppendText(Environment.NewLine);
		}

		public static void AppendTextLine(this TextBoxBase e, params string[] textData)
		{
			foreach (var v in textData)
			{
				e.AppendTextLine(v);
			}
		}

		public static void AppendTextLine(this TextBoxBase e, IEnumerable<string> textData)
		{
			foreach (var v in textData.AsEnumerable())
			{
				e.AppendTextLine(v);
			}
		}

		public static void FadeOut(this UIElement e)
		{
			var a = e.Opacity;

			var t = default(DispatcherTimer);

			t = (1000 / 20).AtInterval(
				delegate
				{
					if (e.Opacity > a)
					{
						t.Stop();
						return;
					}

					a -= 0.09;

					if (a <= 0)
					{
						a = 0;
						t.Stop();
					}
					e.Opacity = a;
				}
			);
		}

		public static SolidColorBrush ToSolidColorBrush(this int argb)
		{
			return ToSolidColorBrush((uint)argb);
		}

		public static SolidColorBrush ToSolidColorBrush(this uint argb)
		{
			var color = new Color();

			color.A = (byte)((argb & 0xff000000) >> 0x18);
			color.R = (byte)((argb & 0xff0000) >> 0x10);
			color.G = (byte)((argb & 0xff00) >> 8);
			color.B = (byte)(argb & 0xff);

			return new SolidColorBrush { Color = color };
		}

		public static DispatcherTimer AtDelay(this int Milliseconds, Action Handler)
		{
			if (Handler == null)
				return null;

			var t = default(DispatcherTimer);

			t = Milliseconds.AtInterval(
				delegate
				{
					Handler();

					t.Stop();
				}
			);

			return t;
		}

		public static DispatcherTimer AtIntervalWithTimer(this int Milliseconds, Action<DispatcherTimer> Handler)
		{
			var t = new DispatcherTimer
			{
				Interval = TimeSpan.FromMilliseconds(Milliseconds)
			};

			t.Tick +=
				delegate
				{
					Handler(t);
				};

			t.Start();

			return t;
		}

		public static DispatcherTimer AtIntervalWithCounter(this int Milliseconds, Action<int> Handler)
		{
			var t = new DispatcherTimer
			{
				Interval = TimeSpan.FromMilliseconds(Milliseconds)
			};

			int Counter = 0;

			t.Tick +=
				delegate
				{
					Handler(Counter);

					Counter++;
				};

			t.Start();

			return t;
		}


		public static DispatcherTimer AtInterval(this int Milliseconds, Action Handler)
		{
			var t = new DispatcherTimer
			{
				Interval = TimeSpan.FromMilliseconds(Milliseconds)
			};

			t.Tick +=
				delegate
				{
					Handler();
				};

			t.Start();

			return t;
		}

		public static T MoveTo<T>(this T e, double x, double y)
			where T : UIElement
		{
			Canvas.SetLeft(e, x);
			Canvas.SetTop(e, y);

			return e;
		}


		public static T MoveTo<T>(this T e, int x, int y)
			where T : UIElement
		{
			Canvas.SetLeft(e, x);
			Canvas.SetTop(e, y);

			return e;
		}

		public static void AttachTo<T>(this T[] e, IAddChild c)
			where T : UIElement
		{
			foreach (var k in e)
			{
				k.AttachTo(c);
			}

		}

		public static T AttachTo<T>(this T e, IAddChild c)
			where T : UIElement
		{
			UIElement x = e;

			c.AddChild(x);

			return e;
		}


		public static T Orphanize<T>(this T e)
			where T : FrameworkElement
		{
			if (e == null)
				return default(T);

			var Panel = e.Parent as Panel;

			if (Panel == null)
				throw new NotImplementedException("Parent should have been a Panel");

			Panel.Children.Remove(e);

			return e;
		}

		public static IEnumerable<T> Orphanize<T>(this IEnumerable<T> e)
				where T : FrameworkElement
		{
			foreach (var v in e)
			{
				v.Orphanize();
			}

			return e;
		}
	}
}
