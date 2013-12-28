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
        #region Max
        public static TResult Max<TSource>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            var value = default(TResult);
            var dirty = false;

            foreach (var v in source.AsEnumerable())
            {
                var x = selector(v);

                if (dirty)
                {
                    if (value < x)
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
    }
}

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq
{
    using TResult = Int64;

    static partial class __Enumerable
    {
        #region Max
        public static TResult Max<TSource>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            var value = default(TResult);
            var dirty = false;

            foreach (var v in source.AsEnumerable())
            {
                var x = selector(v);

                if (dirty)
                {
                    if (value < x)
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
    }
}

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq
{
    using TResult = Int32;

    static partial class __Enumerable
    {
        #region Max
        public static TResult Max<TSource>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            var value = default(TResult);
            var dirty = false;

            foreach (var v in source.AsEnumerable())
            {
                var x = selector(v);

                if (dirty)
                {
                    if (value < x)
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
    }
}

