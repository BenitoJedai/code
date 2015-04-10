using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Linq
{

     static partial class __Enumerable
    {
        public static TSource[] ToArray<TSource>(this IEnumerable<TSource> source)
        {
			// X:\jsc.svn\examples\java\hybrid\JVMCLRLINQSelectByteArray\JVMCLRLINQSelectByteArray\Program.cs

			return source.AsEnumerable().ToList().ToArray();
        }

        public static List<TSource> ToList<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            return new List<TSource>(source);
        }

    }
}
