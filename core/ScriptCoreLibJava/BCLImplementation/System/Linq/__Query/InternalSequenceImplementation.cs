using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLibJava.BCLImplementation.System.Linq.__Query
{
    [Script(Implements = typeof(ScriptCoreLib.Shared.Query.InternalSequence))]
    internal static class __InternalSequenceImplementation
    {

        public static IEnumerable<TSource> AsEnumerable<TSource>(IEnumerable<TSource> source)
        {
            // wrap native types/collections

            return source;
        }
    }
}
