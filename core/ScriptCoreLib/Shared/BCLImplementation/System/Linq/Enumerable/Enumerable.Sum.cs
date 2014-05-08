using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq
{


    static partial class __Enumerable
    {
        public static long Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
        {
            var r = default(long);

            foreach (var i in source.AsEnumerable()) r += selector(i);

            return r;
        }

        public static int Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
        {
            var r = default(int);

            foreach (var i in source.AsEnumerable()) r += selector(i);

            return r;
        }

        public static float Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
        {
            var r = default(float);

            foreach (var i in source.AsEnumerable()) r += selector(i);

            return r;
        }

        public static double Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
        {
            var r = default(double);

            foreach (var i in source.AsEnumerable()) r += selector(i);

            return r;
        }

        public static int Sum(this IEnumerable<int> source)
        {
            var r = default(int);

            foreach (var i in source.AsEnumerable()) r += i;

            return r;
        }

        public static double Sum(this IEnumerable<double> source)
        {
            var r = default(double);

            foreach (var i in source.AsEnumerable()) r += i;

            return r;
        }
    }
}
