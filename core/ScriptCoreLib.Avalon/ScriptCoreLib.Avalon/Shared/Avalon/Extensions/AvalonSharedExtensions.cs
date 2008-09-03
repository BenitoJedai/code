using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Media;

namespace ScriptCoreLib.Shared.Avalon.Extensions
{
	[Script]
	public static class AvalonSharedExtensions
	{
		public static void FadeOut(this UIElement e)
		{
			var a = 1.0;

			var t = default(DispatcherTimer);

			t = (1000 / 15).AtInterval(
				delegate
				{
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
			var Panel = e.Parent as Panel;

			if (Panel == null)
				throw new NotImplementedException("Parent should have been a Panel");

			Panel.Children.Remove(e);

			return e;
		}
	}
}
