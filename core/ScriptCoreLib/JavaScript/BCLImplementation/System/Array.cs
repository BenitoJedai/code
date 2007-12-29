using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib.JavaScript.DOM;
using System.Collections;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{

    [Script(Implements = typeof(global::System.Array))]
    internal class __Array
    {
        /*
        public static IEnumerable<TSource> Sort<TSource>(IEnumerable<TSource> source, Func<TSource, TSource, int> c)
        {
 
        }*/
        /*
        public static void Sort(Array array, IComparer comparer)
        {
        }*/

        public static void Sort<T>(T[] array, Comparison<T> c)
        {
            ((IArray<T>)(object)(array)).sort((a, b) => c(a, b));
        }

        public static void Sort<T>(T[] array, IComparer<T> comparer)
        {
            Sort(array, comparer.Compare);
        }
    }
}
