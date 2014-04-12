using ScriptCoreLib;
using ScriptCoreLib.Shared;

using global::System.Collections;
using global::System.Collections.Generic;

using IDisposable = global::System.IDisposable;

namespace ScriptCoreLibAppJet.JavaScript.Query
{
    [Script(Implements = typeof(ScriptCoreLibAppJet.Shared.Query.InternalSequence))]
    internal static class InternalSequenceImplementation
    {

        public static IEnumerable<TSource> AsEnumerable<TSource>(IEnumerable<TSource> source)
        {
            if (source == null)
                return null;

            var u = ScriptCoreLibAppJet.JavaScript.Runtime.Expando.Of(source);

            if (!u.IsArray)
            {
                if (u.prototype == null)
                {
                    if (ScriptCoreLibAppJet.JavaScript.Runtime.Expando.InternalIsMember(u, "length"))
                    {
                        // DOM list ?
                    }
                    else return source;
                }
                else return source;
            }

            return (ScriptCoreLibAppJet.Shared.Query.SZArrayEnumerator<TSource>)u.To<TSource[]>();
        }
    }


}
