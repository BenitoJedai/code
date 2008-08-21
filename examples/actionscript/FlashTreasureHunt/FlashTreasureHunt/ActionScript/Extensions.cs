using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.display;
using System.IO;

namespace FlashTreasureHunt.ActionScript
{
	/// <summary>
	/// This class defines the extension methods for this project
	/// </summary>
	[Script]
	internal static class Extensions
	{
		public static void ToImages(this IEnumerable<MemoryStream> m, Action<Bitmap[]> h)
		{
			var a = m.ToArray();
			var c = a.Length;
			var n = new Bitmap[c];

			for (int i = 0; i < a.Length; i++)
			{
				var k = i;

				a[k].ToByteArray().LoadBytes<Bitmap>(
					u => 
					{
						n[k] = u;

						c--;

						if (c == 0)
							h(n);
					}
				);
			}
		}
		public static void FadeOutAndOrphanize(this DisplayObject e, int timeout, double step)
		{
			timeout.AtInterval(
			   t =>
			   {
				   if (e.alpha < 0.1)
				   {
					   t.stop();
					   e.Orphanize();
				   }
				   else
				   {
					   e.alpha -= step;
				   }
			   }
		   );
		}


		public static void To(this Point p, double x, double y)
		{
			p.x = x;
			p.y = y;
		}


		public static T Do<T>(this T a, Action<T> e)
		{
			if (e != null)
				e(a);

			return a;
		}


		public static Timer AtInterval(this int e, Action<Timer> a)
		{
			var t = new Timer(e);

			t.timer += delegate { a(t); };

			t.start();

			return t;
		}

		public static T Take<T>(this IEnumerator<T> e)
		{
			if (e.MoveNext())
				return e.Current;


			throw new Exception("source is empty");
		}

		public static T[] Take<T>(this IEnumerator<T> e, int length)
		{
			var a = new T[length];

			for (int i = 0; i < length; i++)
			{
				a[i] = e.Take();
			}

			return a;
		}

		public static T TakeOrDefault<T>(this IEnumerator<T> e)
		{
			var r = default(T);

			if (e.MoveNext())
				r = e.Current;


			return r;
		}

	}
}
