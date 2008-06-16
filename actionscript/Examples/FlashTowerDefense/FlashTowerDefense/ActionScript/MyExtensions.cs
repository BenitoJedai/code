﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.mx.core;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.flash.media;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.ActionScript.Lambda;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.geom;

namespace FlashTowerDefense.ActionScript
{

    [Script]
    public static class MyExtensions
    {
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

        public static void OnHoverUseColor(this TextField e, uint c)
        {
            var n = e.textColor;

            e.mouseOver += i => e.textColor = c;
            e.mouseOut += i => e.textColor = n;
        }

        public static Action<IEnumerable<T>> ToForEach<T>(this Action<T> e)
        {
            return i => i.ForEach(e);

        }

        public static void InvokeRandom(this Action[] e)
        {
            e[e.Length.Random().ToInt32()]();
        }

        public static int FixedRandom(this int e)
        {
            return Convert.ToInt32(((double)e).FixedRandom());
        }

        public static double FixedRandom(this double e)
        {
            if (ByChance_RandomNumbers == null)
                return new Random().NextDouble() * e;

            if (ByChance_RandomNumbers.Count == 0)
                throw new Exception("Need more random numbers!");

            var z = ByChance_RandomNumbers.Dequeue();
            ByChance_RandomNumbers.Enqueue(z);

            return z * e;
        }

        public static Queue<double> ByChance_RandomNumbers;

        public static bool ByChance(this double e)
        {
            if (ByChance_RandomNumbers == null)
                return new Random().NextDouble() < e;

            if (ByChance_RandomNumbers.Count == 0)
                throw new Exception("Need more random numbers!");

            var z = ByChance_RandomNumbers.Dequeue();
            ByChance_RandomNumbers.Enqueue(z);

            return z < e;
        }

        public static Point MoveToArc(this Point n, double arc, double distance)
        {
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

        public static T MoveTo<T>(this T e, double x, double y) where T : DisplayObject
        {
            e.x = x;
            e.y = y;

            return e;
        }

        // todo: how do these methods differ in IL ?
        /*
        public static T MoveToCenter<T>(this T e) where T : DisplayObject
        {
            e.x = -e.width / 2;
            e.y = -e.height / 2;

            return e;
        }
        */
        public static T MoveToCenter<T>(this T e) where T : DisplayObject
        {
            DisplayObject i = e;

            i.x = -i.width / 2;
            i.y = -i.height / 2;

            return e;
        }

        public static int ToInt32(this double e)
        {
            return Convert.ToInt32(e);
        }

        public static T AddTo<T>(this T e, List<T> a)
        {
            a.Add(e);

            return e;
        }

        public static T RemoveFrom<T>(this T e, List<T> a)
        {
            a.Remove(e);

            return e;
        }

        public static Action ToAction(this Sound c)
        {
            return delegate { c.play(); };
        }

        public static void InvokeAtDelays(this Action e, params int[] d)
        {
            foreach (var i in d)
                i.AtDelayDo(e);
        }

        public static Timer AtDelayDoOnRandom(this int e, Action a)
        {
            return e.Random().ToInt32().AtDelayDo(a);
        }

        public static Timer AtDelayDo(this int e, Action a)
        {
            var t = new Timer(e, 1);

            t.timer += delegate { a(); };

            t.start();

            return t;
        }


        public static Timer AtDelay(this int e, Action<Timer> a)
        {
            var t = new Timer(e, 1);

            t.timer += delegate { a(t); };

            t.start();

            return t;
        }


        public static Timer AtIntervalOnRandom(this int e, Action<Timer> a)
        {
            return e.AtInterval(
                t =>
                {
                    e.Random().ToInt32().AtDelay(
                        i =>
                        {
                            if (t.running)
                                a(t);
                        }
                    );
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

        [Script(OptimizedCode = "return new c();")]
        public static object CreateType(this Class c)
        {
            return default(object);
        }

        public static SoundAsset ToSoundAsset(this Class c)
        {
            return (SoundAsset)c.CreateType();
        }

        public static BitmapAsset ToBitmapAsset(this Class c)
        {
            return (BitmapAsset)c.CreateType();
        }

        public static DisplayObject SetCenteredPosition(this DisplayObject e, double x, double y)
        {
            e.x = x - e.width / 2;
            e.y = y - e.height / 2;

            return e;
        }


        public static double Random(this double e)
        {
            return new Random().NextDouble() * e;
        }

        public static double Random(this int e)
        {
            return (Math.Round(new Random().NextDouble() * e));
        }

        public static void Times(this double e, Action h)
        {
            e.Round().Times(h);
        }

        public static void Times(this int e, Action h)
        {
            for (int i = 0; i < e; i++)
            {
                h();
            }
        }

        public static int Round(this double e)
        {
            return (int)(Math.Round(e));
        }
    }
}
