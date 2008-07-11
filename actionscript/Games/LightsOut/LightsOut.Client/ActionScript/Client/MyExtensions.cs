using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.utils;

namespace LightsOut.ActionScript.Client
{
    [Script]
    public static class MyExtensions
    {
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

        public static double GetRotation(this Point p)
        {
            var x = p.x;
            var y = p.y;

            if (x == 0)
                if (y < 0)
                    return System.Math.PI / 2;
                else
                    return (System.Math.PI / 2) * 3;

            var a = System.Math.Atan(y / x);

            if (x < 0)
                a += System.Math.PI;
            else if (y < 0)
                a += System.Math.PI * 2;


            return a;
        }

        public static T MoveToArc<T>(this T e, double arc, double distance) where T : DisplayObject
        {
            DisplayObject n = e;

            n.x += Math.Cos(arc) * distance;
            n.y += Math.Sin(arc) * distance;

            return e;
        }

        public static double Random(this int e)
        {
            return (Math.Round(new Random().NextDouble() * e));
        }


        public static int ToInt32(this bool e)
        {
            if (e)
                return 1;

            return 0;
        }

        public static int ToInt32(this double e)
        {
            return Convert.ToInt32(e);
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


        public static Timer AtInterval(this int e, Action<Timer> a)
        {
            var t = new Timer(e);

            t.timer += delegate { a(t); };

            t.start();

            return t;
        }

        public static void Orphanize(this DisplayObject e)
        {
            if (e.parent != null)
                e.parent.removeChild(e);

        }

        public static Timer AtDelayDo(this int e, Action a)
        {
            var t = new Timer(e, 1);

            t.timer += delegate { a(); };

            t.start();

            return t;
        }


    }


    [Script]
    public class BitField
    {
        public int Value;

        public bool this[int index]
        {
            get
            {
                return ((Value >> index) & 1) == 1;
            }
            set
            {
                Value |= 1 << index;

                if (!value)
                    Value ^= 1 << index;
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
}
