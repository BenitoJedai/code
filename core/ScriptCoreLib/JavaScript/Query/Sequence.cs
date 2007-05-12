using ScriptCoreLib;
using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;

using global::System.Collections;
using global::System.Collections.Generic;

using IDisposable = global::System.IDisposable;

namespace ScriptCoreLib.JavaScript.Query
{
    [Script(Implements = typeof(ScriptCoreLib.Shared.Query.InternalSequence))]
    internal static class InternalSequenceImplementation
    {

        public static IEnumerable<TSource> AsEnumerable<TSource>(IEnumerable<TSource> source)
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
