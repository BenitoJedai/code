using ScriptCoreLib;
using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;

using global::System.Collections;
using global::System.Collections.Generic;

using IDisposable = global::System.IDisposable;
using ScriptCoreLib.Shared.BCLImplementation.System;

namespace ScriptCoreLib.JavaScript.Query
{
    [System.Obsolete("either move the namespace or make automatic as we do with ActionScript")]
    [Script(Implements = typeof(ScriptCoreLib.Shared.BCLImplementation.System.Linq.__Enumerable_AsEnumerable))]
    internal static class __InternalSequenceImplementation
    {

        public static IEnumerable<TSource> AsEnumerable<TSource>(IEnumerable<TSource> source)
        {
            if (source == null)
                return null;

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

            // crude cast
            return (__SZArrayEnumerator<TSource>)(TSource[])(object)source;
        }
    }


}
