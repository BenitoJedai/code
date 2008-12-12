using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Threading;

namespace ScriptCoreLib.Shared.Avalon.Extensions
{
	[Script]
	public static class FadeExtensions
	{

		public static void FadeIn(this UIElement e)
		{
			FadeIn(e, null);
		}

		public static void FadeIn(this UIElement e, Action done)
		{
			var a = e.Opacity;


			(1000 / 20).AtIntervalWithTimer(
				t =>
				{
					if (e.Opacity < a)
					{
						
						if (done != null)
							done();

						e = null;
						t.Stop();
						return;
					}

					a += 0.09;

					if (a >= 1)
					{
						e.Opacity = 1;
						if (done != null)
							done();

						e = null;
						t.Stop();
						return;
					}
					e.Opacity = a;
				}
			);
		}

		public static void FadeOut(this UIElement e)
		{
			FadeOut(e, null);
		}


		public static void FadeOut(this UIElement e, Action done)
		{
			var a = e.Opacity;

			(1000 / 20).AtIntervalWithTimer(
				t =>
				{
					if (e.Opacity > a)
					{
						if (done != null)
							done();

						e = null;
						t.Stop();
						return;
					}

					a -= 0.09;

					if (a <= 0)
					{
						e.Opacity = 0;
						if (done != null)
							done();

						e = null;
						t.Stop();
						return;
					}
					e.Opacity = a;
				}
			);
		}

	}
}
