using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Collections.Generic
{
    [Script(Implements = typeof(global::System.Collections.Generic.Comparer<>))]
    internal abstract class __Comparer<T> : __IComparer<T>, __IComparer
    {

        public abstract int Compare(T x, T y);

        int __IComparer.Compare(object x, object y)
        {
            // fallback to nongenerics
            return __Comparer.Default.Compare(x, y);
        }

        static __Comparer<T> defaultComparer;
        public static __Comparer<T> Default
        {
            get
            {
                __Comparer<T> e = defaultComparer;
                if (e == null)
                {
                    e = new __GenericComparer();
                    defaultComparer = e;
                }
                return e;
            }
        }



        [Script]
        class __GenericComparer : __Comparer<T>
        {
            public override int Compare(T x, T y)
            {
                // fallback to nongenerics
                return __Comparer.Default.Compare(x, y);
            }
        }
    }
}
