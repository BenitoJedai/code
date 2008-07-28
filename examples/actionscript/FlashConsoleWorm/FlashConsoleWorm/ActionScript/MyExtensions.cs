using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.display;

namespace FlashConsoleWorm.ActionScript
{
    /// <summary>
    /// This class defines the extension methods for this project
    /// </summary>
    [Script]
    internal static class MyExtensions
    {
		public static T MoveToArc<T>(this T e, double arc, double distance) where T : DisplayObject
		{
			DisplayObject n = e;

			n.x += Math.Cos(arc) * distance;
			n.y += Math.Sin(arc) * distance;

			return e;
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
    }

	[Script]
	class ShapeWithMovement : Shape
	{
		Point MoveToTarget = new Point();

		public ShapeWithMovement MoveTo(double x, double y)
		{
			MoveToTarget = new Point { x = x, y = y };


			return this;
		}

		public ShapeWithMovement()
		{
			(1000 / 30).AtInterval(
				t =>
				{
					var c = this.ToPoint();

					var x = MoveToTarget - c;

					if (x.length < 2)
					{
					}
					else if (x.length < 4)
					{
						this.MoveToArc(x.GetRotation(), x.length / 2);
					}
					else
					{
						this.MoveToArc(x.GetRotation(), x.length / 4);
					}
				}
			);
		}

	
	}

	[Script]
	public class Property<T>
	{
		public event Action ValueChanged;

		T _Value;

		public T Value { get { return _Value; } set { _Value = value; if (ValueChanged != null) ValueChanged(); } }
	}

	[Script]
	public class BooleanProperty : Property<bool>
	{
		public event Action ValueChangedToTrue;
		public event Action ValueChangedToFalse;

		public BooleanProperty()
		{
			this.ValueChanged +=
				delegate
				{
					if (this.Value)
					{
						if (ValueChangedToTrue != null)
							ValueChangedToTrue();
					}
					else
					{
						if (ValueChangedToFalse != null)
							ValueChangedToFalse();
					}
				};
		}

		public static implicit operator bool(BooleanProperty e)
		{
			return e.Value;
		}

		public static implicit operator BooleanProperty(bool e)
		{
			return new BooleanProperty { Value = e };
		}
	}

}
