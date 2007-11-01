using ScriptCoreLib;
using ScriptCoreLib.Shared;

using global::System.Collections;
using global::System.Collections.Generic;

using IDisposable = global::System.IDisposable;
using System;

namespace ScriptCoreLib.Shared.Query
{

    internal static partial class __Enumerable
    {
        public static int Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
        {
            var r = default(int);

            foreach (var i in source.AsEnumerable()) r += selector(i);

            return r;
        }

        public static int Sum(this IEnumerable<int> source)
        {
            var r = default(int);

            foreach (var i in source.AsEnumerable()) r += i;

            return r;
        }
    }
}
