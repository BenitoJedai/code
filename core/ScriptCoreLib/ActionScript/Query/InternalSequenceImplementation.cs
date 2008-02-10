using ScriptCoreLib;
using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;

using global::System.Collections;
using global::System.Collections.Generic;

using IDisposable = global::System.IDisposable;

namespace ScriptCoreLib.ActionScript.Query
{
    [Script(Implements = typeof(ScriptCoreLib.Shared.Query.InternalSequence))]
    internal static class InternalSequenceImplementation
    {

        public static IEnumerable<TSource> AsEnumerable<TSource>(IEnumerable<TSource> source)
        {
            return source;

            //if (source == null)
            //    return null;

            // fixme: works only as array to IEnumerable

            /*
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
            }*/

            //return (ScriptCoreLib.Shared.Query.SZArrayEnumerator<TSource>) ToArray(source);
        }



        //[Script(OptimizedCode = "return o;")]
        //private static TSource[] ToArray<TSource>(IEnumerable<TSource> o)
        //{
        //    return default(TSource[]);
        //}
    }


}
