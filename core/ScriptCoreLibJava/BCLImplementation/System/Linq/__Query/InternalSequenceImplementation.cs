using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLibJava.BCLImplementation.System.Linq.__Query
{
    [Script(Implements = typeof(ScriptCoreLib.Shared.BCLImplementation.System.Linq.__Enumerable_AsEnumerable))]
    internal static class __InternalSequenceImplementation
    {

        public static IEnumerable<TSource> AsEnumerable<TSource>(IEnumerable<TSource> source)
        {
            // wrap native types/collections
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201408/20140810/asenumerable/async-byref-struct

            return source;
        }
    }
}
