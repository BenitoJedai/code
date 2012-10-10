using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Drawing;

namespace NatureBoyTestPad.js
{
    [Script]
    static class Extensions
    {
        public static Func<A, B, C, T> ToFunc<A, B, C, T>(this T e, Func<A, B, C, T> x)
        {
            return x;
        }

        public static Func<A, B, T> ToFunc<A, B, T>(this T e, Func<A, B, T> x)
        {
            return x;
        }

        public static Func<A, T> ToFunc<A, T>(this T e, Func<A, T> x)
        {
            return x;
        }

        public static void Throw(this string e)
        {
            throw new Exception(e);
        }

        public static Point ToInt32(this Point<double> i)
        {
            return new Point( System.Convert.ToInt32(i.X), System.Convert.ToInt32(i.Y));
        }

        public static Point<double> ToDouble(this Point i)
        {
            return new Point<double> { X = i.X, Y = i.Y };
        }


        public static int Random(this int i)
        {
            return new Random().Next(i);
        }
    }
}
