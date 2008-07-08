using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Collections
{
    [Script(Implements = typeof(Comparer))]
    internal class __Comparer : __IComparer
    {
        static __Comparer()
        {
            Default = new __Comparer();
        }

        public static readonly __Comparer Default;

        public static bool IsType(Type t, object a, object b)
        {
            if (!a.GetType().Equals(t))
                return false;

            if (!b.GetType().Equals(t))
                return false;

            return true;
        }

        #region __IComparer Members

        public int Compare(object a, object b)
        {
            if (a == b)
            {
                return 0;
            }
            if (a == null)
            {
                return -1;
            }
            if (b == null)
            {
                return 1;
            }

            #region native datatypes, not defined bt BCL
            if (IsType(typeof(int), a, b))
            {
                var x = (int)a;
                var y = (int)b;

                return x.CompareTo(y);
            }

            if (IsType(typeof(uint), a, b))
            {
                var x = (uint)a;
                var y = (uint)b;

                return x.CompareTo(y);
            }

            if (IsType(typeof(double), a, b))
            {
                var x = (double)a;
                var y = (double)b;

                return x.CompareTo(y);
            }

            if (IsType(typeof(bool), a, b))
            {
                var x = (bool)a;
                var y = (bool)b;

                return x.CompareTo(y);
            }

            if (IsType(typeof(string), a, b))
            {
                var x = (string)a;
                var y = (string)b;

                return x.CompareTo(y);
            }
            #endregion


            IComparable comparable = a as IComparable;
            if (comparable == null)
            {
                throw new ArgumentException("ImplementIComparable");
            }
            return comparable.CompareTo(b);

        }

        #endregion
    }
}
