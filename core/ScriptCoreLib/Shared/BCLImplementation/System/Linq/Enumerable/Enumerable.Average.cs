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
        #region Min
        public static TResult Min<TSource>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            var value = default(TResult);
            var dirty = false;

            foreach (var v in source.AsEnumerable())
            {
                var x = selector(v);

                if (dirty)
                {
                    if (value > x)
                        value = x;
                }
                else
                {
                    dirty = true;
                    value = x;
                }
            }

            if (!dirty)
                throw __DefinedError.NoElements();

            return value;
        }
        #endregion

        #region Min(this IEnumerable<TResult>)
        public static TResult Min(this IEnumerable<TResult> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            var num2 = default(TResult);
            bool flag2 = false;
            foreach (TResult num3 in source.AsEnumerable())
            {
                if (flag2)
                {
                    if (num3 < num2)
                    {
                        num2 = num3;
                    }
                    continue;
                }
                num2 = num3;
                flag2 = true;
            }
            if (!flag2)
            {
                throw __DefinedError.NoElements();
            }
            return num2;
        }
        #endregion
    }
}

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq
{
    using TResult = Int64;

    static partial class __Enumerable
    {
        #region Min
        public static TResult Min<TSource>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            var value = default(TResult);
            var dirty = false;

            foreach (var v in source.AsEnumerable())
            {
                var x = selector(v);

                if (dirty)
                {
                    if (value > x)
                        value = x;
                }
                else
                {
                    dirty = true;
                    value = x;
                }
            }

            if (!dirty)
                throw __DefinedError.NoElements();

            return value;
        }
        #endregion

        #region Min(this IEnumerable<TResult>)
        public static TResult Min(this IEnumerable<TResult> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            var num2 = default(TResult);
            bool flag2 = false;
            foreach (TResult num3 in source.AsEnumerable())
            {
                if (flag2)
                {
                    if (num3 < num2)
                    {
                        num2 = num3;
                    }
                    continue;
                }
                num2 = num3;
                flag2 = true;
            }
            if (!flag2)
            {
                throw __DefinedError.NoElements();
            }
            return num2;
        }
        #endregion
    }
}

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq
{
    using TResult = Int32;

    static partial class __Enumerable
    {
        #region Min
        public static TResult Min<TSource>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            var value = default(TResult);
            var dirty = false;

            foreach (var v in source.AsEnumerable())
            {
                var x = selector(v);

                if (dirty)
                {
                    if (value > x)
                        value = x;
                }
                else
                {
                    dirty = true;
                    value = x;
                }
            }

            if (!dirty)
                throw __DefinedError.NoElements();

            return value;
        }
        #endregion

        #region Min(this IEnumerable<TResult>)
        public static TResult Min(this IEnumerable<TResult> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            var num2 = default(TResult);
            bool flag2 = false;
            foreach (TResult num3 in source.AsEnumerable())
            {
                if (flag2)
                {
                    if (num3 < num2)
                    {
                        num2 = num3;
                    }
                    continue;
                }
                num2 = num3;
                flag2 = true;
            }
            if (!flag2)
            {
                throw __DefinedError.NoElements();
            }
            return num2;
        }
        #endregion

    }
}

