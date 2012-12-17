using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]
namespace TestOrderedEnumerableConstructor
{
    [Script]
    internal static partial class __Enumerable
    {
        public static __OrderedEnumerable<TSource, TKey> OrderBy<TSource, TKey>(object source, object keySelector)
        {
            return new __OrderedEnumerable<TSource, TKey>(source, keySelector, null, false);
        }

    }

    [Script]
    internal class __OrderedEnumerable<TSource, TKey> // : __OrderedEnumerable<TSource>, IOrderedEnumerable<TSource>
    {
        internal __OrderedEnumerable()
            : this(null, null, null, false)
        {

        }

        public __OrderedEnumerable(object source, object keySelector, object comparer, bool descending)
        {
        }
    }

}
