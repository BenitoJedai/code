using ScriptCoreLib;
using ScriptCoreLib.Shared.BCLImplementation.System.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLibJava.BCLImplementation.System.Collections
{
    // http://referencesource.microsoft.com/#mscorlib/system/collections/comparer.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Collections\Comparer.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Collections\Comparer.cs


    [Script(Implements = typeof(Comparer))]
    internal class __Comparer : __IComparer
    {
        static __Comparer()
        {
            Default = new __Comparer();
        }

        public static readonly __Comparer Default;


        static Type DoubleType = typeof(Double);

        static Type Int64Type = typeof(long);

        static Type Int32Type = typeof(int);

        static Type UInt32Type = typeof(uint);
        static Type StringType = typeof(string);
        static Type BooleanType = typeof(bool);


        public static bool IsNumber(object x)
        {
            if (x is double)
                return true;

            if (x is int)
                return true;

            if (x is long)
                return true;

            if (x is uint)
                return true;

            return false;
        }

        public static bool IsNumber(object x, object y)
        {
            if (x is double)
            {
                return IsNumber(y);
            }

            if (y is double)
            {
                return IsNumber(x);
            }

            return false;
        }

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



            if (IsNumber(a, b))
            {
                var x = (double)a;
                var y = (double)b;

                return x.CompareTo(y);
            }

            if (a is long)
                if (b is long)
                {
                    var x = (long)a;
                    var y = (long)b;

                    return x.CompareTo(y);
                }


            if (a is int)
                if (b is int)
                {
                    var x = (int)a;
                    var y = (int)b;

                    return x.CompareTo(y);
                }


            if (a is bool)
                if (b is bool)
                {
                    var x = (bool)a;
                    var y = (bool)b;

                    return x.CompareTo(y);
                }

            if (a is string)
                if (b is string)
                {
                    var x = (string)a;
                    var y = (string)b;

                    return x.CompareTo(y);
                }

            // uint does not exist in jvm, will conflict with int/long
            if (a is uint)
                if (b is uint)
                {
                    var x = (uint)a;
                    var y = (uint)b;

                    return x.CompareTo(y);
                }

            #endregion


            IComparable comparable = a as IComparable;
            if (comparable == null)
            {
                throw new ArgumentException("Implement IComparable for " + a.GetType().FullName + " vs " + b.GetType().FullName);
            }
            return comparable.CompareTo(b);

        }

        #endregion
    }

}
