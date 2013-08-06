using ScriptCoreLib;
using ScriptCoreLib.Shared;

using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;

using global::System.Collections;
using global::System.Collections.Generic;

using IDisposable = global::System.IDisposable;

namespace ScriptCoreLib.ActionScript.Query
{
    
    [Script(Implements = typeof(ScriptCoreLib.Shared.BCLImplementation.System.Linq.__Enumerable_AsEnumerable))]
    internal static class __InternalSequenceImplementation
    {

        public static IEnumerable<TSource> AsEnumerable<TSource>(IEnumerable<TSource> source)
        {
            // wrap native types/collections

            return source;
        }
    }


}
