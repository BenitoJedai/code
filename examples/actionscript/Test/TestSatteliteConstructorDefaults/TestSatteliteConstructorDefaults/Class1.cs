using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
[assembly: Obfuscation(Feature = "script")]

namespace TestSatteliteConstructorDefaults
{
    internal class __OrderedEnumerable<TSource, TKey>
    {

        internal __OrderedEnumerable()
            : this(null, false)
        {

        }

        public __OrderedEnumerable(object comparer, bool descending)
        {

        }

        static void foo()
        {
            var x = new __OrderedEnumerable<object, bool>();

        }
    }
}
