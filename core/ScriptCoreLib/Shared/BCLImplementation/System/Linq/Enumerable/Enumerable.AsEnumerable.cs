using ScriptCoreLib;
using ScriptCoreLib.Shared;
using System;
using System.Collections.Generic;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq
{
     static partial class __Enumerable
    {
        //[Script(NotImplementedHere = true)]
        public static IEnumerable<TSource> AsEnumerable<TSource>(this IEnumerable<TSource> source)
        {
            // !! AsEnumerable is defined in another language selection in another assembly
            // the runtime should create a SZArray for arrays but it does not for the moment

            // NoOptimization();

            return __Enumerable_AsEnumerable.AsEnumerable(source);
        }
    }

    public static class __Enumerable_AsEnumerable
    {
        // this class is will not be translated to the target language.
        // each language can do special checks here to convert from native enumerations

        public static IEnumerable<TSource> AsEnumerable<TSource>(IEnumerable<TSource> source)
        {
            return source;

        }
    }
}
