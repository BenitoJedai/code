using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;
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
	public static class Extensions
	{
		public static double GetDistance(this Point a, Point b)
		{
			var x = a.x - b.x;
			var y = a.y - b.y;

			return Math.Sqrt(x * x + y * y);
		}

		public static void drawLine(this BitmapData e, uint color, double x, double y, double cx, double cy)
		{
			e.drawLine(color, (int)x, (int)y, (int)cx, (int)cy);
		}
		public static void drawLine(this BitmapData e, uint color, int x, int y, int cx, int cy)
		{
			e.@lock();



			var dx = cx - x;
			var dy = cy - y;


			Action<int, int> setPixel = (mul, div) =>
					e.setPixel32((x + dx * mul / div), (y + dy * mul / div), color);

			var len = new Point { x = dx, y = dy }.length.Floor().Min(64);

			if (len > 2)
			{
				for (int i = 0; i < len + 1; i++)
				{
					setPixel(i, len);
				}
			}
			else
			{
				setPixel(0, 1);
				setPixel(1, 1);
			}



			e.unlock();
		}


		public static DisplayObject GetStageChild(this DisplayObject e)
		{
			DisplayObject r = null;
			DisplayObject p = e;

			while (r == null)
			{
				if (p.parent == p.stage)
				{
					r = p;
				}
				else
				{
					p = p.parent;
				}
			}

			return r;
		}

		public static IEnumerable<DisplayObject> Siblings(this DisplayObject e)
		{
			return e.parent.Children().Where(k => k != e);
		}

		public static void InvokeWhenStageIsReady(this DisplayObject o, Action a)
		{
			if (o.stage == null)
				o.addedToStage +=
					delegate
					{
						a();
					};
			else
				a();
		}


		[Script]
		public class DelayChain
		{
			// should actually be immutable type

			public int Delay;

			public readonly Queue<Action> Actions = new Queue<Action>();
		}

		public static DelayChain Chain(this int delay, Action handler)
		{
			return new DelayChain { Delay = delay }.Chain(handler);
		}

		public static DelayChain Chain(this DelayChain e, Action handler)
		{
			e.Actions.Enqueue(handler);

			return e;
		}


		public static Timer Do(this DelayChain e)
		{
			return e.Delay.AtInterval(
				t =>
				{
					if (e.Actions.Count == 0)
					{
						t.stop();
						return;
					}

					e.Actions.Dequeue()();
				}
			);
		}


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



						   if (done != null)
							   done();

						   t.stop();

						   return;
					   }
					   e.alpha += step;
				   }
			   );
		}

		public static void FadeOut(this DisplayObject e)
		{
			FadeOut(e, 1000 / 15, 0.1, null
				);

		}

		public static void FadeOut(this DisplayObject e, Action done)
		{
			FadeOut(e, 1000 / 15, 0.1, done
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


					   if (done != null)
						   done();

					   t.stop();
					   return;
				   }
				   e.alpha -= step;
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

		public static IEnumerator<TResult> Select<T, TResult>(this IEnumerator<T> e, Func<T, TResult> selector)
		{
			return new DynamicEnumerator<TResult>
			{
				VirtualCurrent = () => selector(e.Current),
				VirtualDispose = () => e.Dispose(),
				VirtualMoveNext = () => e.MoveNext()

			};
		}

		[Script]
		class DynamicEnumerator<T> : IEnumerator<T>
		{
			public Func<T> VirtualCurrent;
			public Action VirtualDispose;
			public Func<bool> VirtualMoveNext;

			#region IEnumerator<T> Members

			public T Current
			{
				get { return VirtualCurrent(); }
			}

			#endregion

			#region IDisposable Members

			public void Dispose()
			{
				VirtualDispose();
			}

			#endregion

			#region IEnumerator Members

			object System.Collections.IEnumerator.Current
			{
				get { return Current; }
			}

			public bool MoveNext()
			{
				return VirtualMoveNext();
			}

			public void Reset()
			{
				throw new NotImplementedException();
			}

			#endregion
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
