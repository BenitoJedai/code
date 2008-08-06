using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.media;
using ScriptCoreLib.ActionScript.flash.filters;

namespace FlashSpaceInvaders.ActionScript.Extensions
{
	[Script]
	public static class MyExtensions
	{
		public static T ApplyFilter<T>(this T e, BitmapFilter f)  where T : DisplayObject
		{
			if (f != null)
			{
				var n = (DisplayObject)e;

				n.filters = new[] { f };
			}

			return e;
		}

		public static SoundChannel play(this Class c)
		{
			return c.ToSoundAsset().play();
		}
		public static T TeleportTo<T>(this T e, double x, double y) where T : SpriteWithMovement
		{
			SpriteWithMovement n = e;

			n.x = x;
			n.y = y;

			n.MoveToTarget.Value = new Point { x = x, y = y };


			return e;
		}

		public static void appendTextLine(this TextField t, string e)
		{
			t.appendText(e + Environment.NewLine);

		}
		public static IEnumerable<T> Concat<T>(this IEnumerable<T> e, T u)
		{
			return e.Concat(new[] { u });

		}
		public static T Do<T>(this T e, Action<T> handler)
		{
			handler(e);

			return e;

		}

		public static ParentRelation<TElement, TParent> WithParent<TElement, TParent>(this TElement e, TParent p)
		{
			return new ParentRelation<TElement, TParent> { Element = e, Parent = p };
		}

		public static Point MoveToArc(this Point e, double arc, double distance)
		{
			var n = new Point(e.x, e.y);

			n.x += Math.Cos(arc) * distance;
			n.y += Math.Sin(arc) * distance;

			return n;
		}

		public static T MoveToArc<T>(this T e, double arc, double distance) where T : DisplayObject
		{
			DisplayObject n = e;

			n.x += Math.Cos(arc) * distance;
			n.y += Math.Sin(arc) * distance;

			return e;
		}
		public static Action FadeOutAndOrphanize(this DisplayObject e)
		{
			return FadeOutAndOrphanize(e, 1000 / 30, 0.08);
		}

		public static Action FadeOutAndOrphanize(this DisplayObject e, int timeout, double step)
		{
			var r = timeout.AtInterval(
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

			return () => r.stop();

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

		public static Timer AtDelayDo(this int e, Action a)
		{
			var t = new Timer(e, 1);

			t.timer += delegate { a(); };

			t.start();

			return t;
		}

		public static bool IsEqual(this Point p, Point x)
		{
			if (p.x != x.x)
				return false;

			if (p.y != x.y)
				return false;

			return true;
		}

		public static Timer AtInterval(this int e, Action<Timer> a)
		{
			var t = new Timer(e);

			t.timer += delegate { a(t); };

			t.start();

			return t;
		}



		public static int Random(this int i)
		{
			return new Random().Next(i);
		}


		public static void Times(this int e, Action h)
		{
			for (int i = 0; i < e; i++)
			{
				h();
			}
		}

		public static T AnimateAt<T>(this T c, DisplayObject[] e, int interval) where T : DisplayObjectContainer
		{
			var i = 0;

			Action update =
				delegate
				{
					e[i].x = -e[i].width / 2;
					e[i].y = -e[i].height / 2;
				};

			update();

			c.addChild(e[i]);

			if (e.Length > 1)
			{
				var t = new Timer(interval);


				t.timer +=
					delegate
					{
						c.removeChild(e[i]);

						i = (i + 1) % e.Length;

						update();

						c.addChild(e[i]);


					};

				t.start();
			}

			return c;
		}
	}
}
