using ScriptCoreLib;
using ScriptCoreLib.Shared;
using ScriptCoreLib.Shared.Query;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;

using global::System.Collections;
using global::System.Collections.Generic;

using IDisposable = global::System.IDisposable;
using ScriptException = global::ScriptCoreLib.JavaScript.System.ScriptException;

namespace ScriptCoreLib.JavaScript.Query
{
    [Script(Implements = typeof(ScriptCoreLib.Shared.Query.InternalSequence))]
    internal static class InternalSequenceImplementation
    {
        public static TSource[] ToArray<TSource>(IEnumerable<TSource> source)
        {
            var a = new IArray<TSource>();

            foreach (var v in source.AsEnumerable())
            {
                a.push(v);
            }

            return a.ToArray();
        }

        public static IEnumerable<TSource> AsEnumerable<TSource>(this IEnumerable<TSource> source)
        {
            var u = ScriptCoreLib.JavaScript.Runtime.Expando.Of(source);

            if (!u.IsArray)
            {
                if (u.prototype == null)
                {
                    if (ScriptCoreLib.JavaScript.Runtime.Expando.InternalIsMember(u, "length"))
                    {
                        // DOM list ?
                    }
                    else return source;
                }
                else return source;
            }

            return (ScriptCoreLib.Shared.Query.SZArrayEnumerator<TSource>)u.To<TSource[]>();
        }
    }


}
