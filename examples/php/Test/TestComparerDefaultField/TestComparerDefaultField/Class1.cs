using ScriptCoreLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
[assembly: ScriptTypeFilter(ScriptType.PHP), Script]

namespace TestComparerDefaultField
{


    [Script(Implements = typeof(Comparer))]
    internal class __Comparer
    {
        static __Comparer()
        {
            Default = new __Comparer();
        }

        public int Compare(object ka, object kb)
        {
            return 0;
        }

        public static __Comparer Default;
    }

    [Script]
    class __GenericComparer<T>
    {
        public void Compare(T x, T y)
        {
            // fallback to nongenerics

            var a = __Comparer.Default.Compare(x, y);
            __Comparer.Default = null;

            var b = Comparer.Default.Compare(x, y);
            //XComparer.Default = null;
        }
    }
}
