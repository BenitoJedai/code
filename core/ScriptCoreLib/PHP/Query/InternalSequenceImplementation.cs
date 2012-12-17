using ScriptCoreLib;
using ScriptCoreLib.Shared;


using global::System.Collections;
using global::System.Collections.Generic;

using IDisposable = global::System.IDisposable;
using ScriptCoreLib.Shared.BCLImplementation.System;

namespace ScriptCoreLib.PHP.Query
{
    [Script(Implements = typeof(ScriptCoreLib.Shared.BCLImplementation.System.Linq.__Enumerable_AsEnumerable))]
    internal static class __InternalSequenceImplementation
    {

        public static IEnumerable<TSource> AsEnumerable<TSource>(IEnumerable<TSource> source)
        {
            if (source == null)
                return null;

            if (Native.API.is_array(source))
            {
                // crude cast
                return (__SZArrayEnumerator<TSource>)(TSource[])(object)source;
                // wrap native types/collections
            }

            return source;
        }
    }


}
