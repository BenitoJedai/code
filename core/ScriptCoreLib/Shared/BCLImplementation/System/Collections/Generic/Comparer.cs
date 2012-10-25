using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using ScriptCoreLib.Shared.BCLImplementation.System.Collections;
using ScriptCoreLib.Shared.BCLImplementation.System.Collections.Generic;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Collections.Generic
{
    [Script(Implements = typeof(global::System.Collections.Generic.Comparer<>))]
    internal abstract class __Comparer<T> : __IComparer<T>
        //, __IComparer
    {
        // K:\staging\web\java\ScriptCoreLib\Shared\BCLImplementation\System\Collections\Generic\__Comparer_1.java:23: Compare(T,T) is already defined in ScriptCoreLib.Shared.BCLImplementation.System.Collections
        //.Generic.__Comparer_1
        //    public final  int Compare(Object x, Object y)

        public abstract int Compare(T x, T y);

        // will this work?
        //public int Compare(object x, object y)
        //{
        //    // fallback to nongenerics
        //    return Comparer.Default.Compare(x, y);
        //}

        // generic static fields not yet supported
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/20121001-solutionbuilderv1/20121024-linq
        //static __Comparer<T> defaultComparer;
        public static __Comparer<T> Default
        {
            get
            {
                __Comparer<T> e = new __GenericComparer();
                return e;
            }
        }



        [Script]
        class __GenericComparer : __Comparer<T>
        {
            public override int Compare(T x, T y)
            {
                // fallback to nongenerics
                return Comparer.Default.Compare(x, y);
            }
        }
    }
}
