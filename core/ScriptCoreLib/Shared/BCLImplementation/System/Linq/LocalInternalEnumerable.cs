using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq
{
    // tested by?
    internal static class LocalInternalEnumerable
    {
        // this class is used to dispatch implementation to multiple languages
        // inside this assembly

        public static IEnumerable<TSource> Sort<TSource>(IEnumerable<TSource> source, Func<TSource, TSource, int> c)
        {
            throw new NotImplementedException();
        }

    
    }
}
