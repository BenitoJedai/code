using ScriptCoreLib;
using ScriptCoreLib.Shared;

using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;

using global::System.Collections.Generic;

namespace LINQPreviewScriptApplication.js
{
    [Script]
    internal static class SequenceWrapper
    {
        // LINQ may 2006 CTP seems to have some issues if the extension class
        // is inside another assembly which is not the System.Query.dll
        //
        // it seems to be the same for March CTP 2007, i wonder why
        //
        // current version of jsc cannot provide IEnumerable<T> implementation
        // for an array like T[], use method overloading as a workaround

        public static IEnumerable<S> Select<T, S>(this T[] source, Func<T, S> selector)
        {
            return ScriptCoreLib.Shared.Query.Sequence.Select(source, selector);
        }

        public static IEnumerable<S> Select<T, S>(this IEnumerable<T> source, Func<T, S> selector)
        {
            return ScriptCoreLib.Shared.Query.Sequence.Select(source, selector);
        }

        public static IEnumerable<T> Where<T>(this T[] source, Func<T, bool> predicate)
        {
            return ScriptCoreLib.Shared.Query.Sequence.Where(source, predicate);
        }

        public static IEnumerable<T> Where<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            return ScriptCoreLib.Shared.Query.Sequence.Where(source, predicate);
        }

        /* is missing from current query dll
         * 
        public static int Count<T>(this T[] source)
        {
            return ScriptCoreLib.Shared.Query.Sequence.Count(source);
        }*/

        public static int Count<T>(this IEnumerable<T> source)
        {
            return ScriptCoreLib.Shared.Query.Sequence.Count(source);
        }

    }

    [Script]
    public delegate T FuncParams<P, T>(params P[] p);

    [Script]
    public delegate T FuncParams<A0, P, T>(A0 a0, params P[] p);

    [Script]
    public static class Lambadas
    {
        public static T InlineIf<T>(bool b, Func<T> t, Func<T> f)
        {
            if (b)
                return t();

            return f();
        }

        [Script]
        public delegate Func<A0, T> YDelegate<A0, T>(Func<A0, T> e);
        [Script]
        public delegate Func<A0, A1, T> YDelegate<A0, A1, T>(Func<A0, A1, T> e);


        public static Func<A0, T> Y<A0, T>(this YDelegate<A0, T> le)
        {
            var me = default(Func<A0, T>); return me = p => le(me)(p);
        }

        public static Func<A0, A1, T> Y<A0, A1, T>(this YDelegate<A0, A1, T> le)
        {
            var me = default(Func<A0, A1, T>); return me = ( a0, a1) => le(me)(a0, a1);
        }

        #region FixLastParam

        public static Func<T> Fix<A0, T>(this Func<A0, T> f, A0 a0)
        {
            return () => f(a0);
        }

        public static Func<A0, T> Fix<A0, A1, T>(this Func<A0, A1, T> f, A1 a1)
        {
            return ( a0) => f(a0, a1);
        }

        public static Func<A0, A1, T> Fix<A0, A1, A2, T>(this Func<A0, A1, A2, T> f, A2 a2)
        {
            return ( a0, a1) => f(a0, a1, a2);
        }

        #endregion
    }

}
