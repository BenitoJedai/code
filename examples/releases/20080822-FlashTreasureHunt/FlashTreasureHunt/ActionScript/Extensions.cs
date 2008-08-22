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
using ScriptCoreLib.ActionScript.flash.events;

namespace FlashTreasureHunt.ActionScript
{
	/// <summary>
	/// This class defines the extension methods for this project
	/// </summary>
	[Script]
	internal static class Extensions
	{
		public static int Floor(this double e)
		{
			return (int)Math.Floor(e);
		}

		public static Timer AtDelayDo(this int e, Action a)
		{
			var t = new Timer(e, 1);

			t.timer += delegate { a(); };

			t.start();

			return t;
		}

		public static Point MoveToArc(this Point e, double direction, double distance)
		{
			var p = new Point(e.x, e.y);
			p.x += Math.Cos(direction) * distance;
			p.y += Math.Sin(direction) * distance;

			return p;
		}


		public static Point ToStagePoint(this MouseEvent m)
		{
			return new Point(m.stageX, m.stageY);
		}

		public static void FadeIn(this DisplayObject e)
		{
			FadeIn(e, 1000 / 15, 0.1, null);
		}


		public static void FadeIn(this DisplayObject e, Action done)
		{
			FadeIn(e, 1000 / 15, 0.1, done);
		}

		public static void FadeIn(this DisplayObject e, int interval, double step, Action done)
		{
			interval.AtInterval(
				   t =>
				   {
					   if (e.alpha > 0.9)
					   {
						   e.alpha = 1;

						   t.stop();

						   if (done != null)
							   done();
					   }
					   else
					   {
						   e.alpha += step;
					   }
				   }
			   );
		}

		public static void FadeOut(this DisplayObject e, int timeout, double step, Action done)
		{
			timeout.AtInterval(
			   t =>
			   {
				   if (e.alpha < 0.1)
				   {
					   e.alpha = 0;

					   t.stop();
					   done();
				   }
				   else
				   {
					   e.alpha -= step;
				   }
			   }
		   );
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

		public static R Do<T, R>(this T a, Func<T, R> e)
		{
			return e(a);

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
