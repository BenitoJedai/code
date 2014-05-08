using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq
{
    using TResult = Double;

    static partial class __Enumerable
    {
        #region Average
        public static double Average<TSource>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            //public static <TSource> double Average_0600046d(__IEnumerable_1<TSource> source, __Func_2<TSource, Double> selector)
            //{
            //    return  __Enumerable.Average_060004a3(__Enumerable.<TSource, Double>Select(source, selector));
            //}

            return source.Select<TSource, TResult>(selector).Average();
        }

        public static double Average(this IEnumerable<TResult> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            var num = default(double);
            var num2 = 0;
            foreach (var num3 in source.AsEnumerable())
            {
                num += num3;
                num2++;
            }
            if (num2 <= 0)
            {
                throw __DefinedError.NoElements();
            }
            return (((double)num) / ((double)num2));
        }
        #endregion


    }
}

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq
{
    using TResult = Int64;

    static partial class __Enumerable
    {
        #region Average
        public static double Average<TSource>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            return source.Select<TSource, TResult>(selector).Average();
        }

        public static double Average(this IEnumerable<TResult> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            long num = 0L;
            long num2 = 0L;
            foreach (var num3 in source.AsEnumerable())
            {
                num += num3;
                num2 += 1L;
            }
            if (num2 <= 0L)
            {
                throw __DefinedError.NoElements();
            }
            return (((double)num) / ((double)num2));
        }
        #endregion


    }
}

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq
{
    using TResult = Int32;

    static partial class __Enumerable
    {
        #region Average
        public static double Average<TSource>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            return source.Select<TSource, TResult>(selector).Average();
        }

        public static double Average(this IEnumerable<TResult> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            long num = 0L;
            long num2 = 0L;
            foreach (var num3 in source.AsEnumerable())
            {
                num += num3;
                num2 += 1L;
            }
            if (num2 <= 0L)
            {
                throw __DefinedError.NoElements();
            }
            return (((double)num) / ((double)num2));
        }
        #endregion


    }
}

