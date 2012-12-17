using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using ScriptCoreLib.PHP.Runtime;
using ScriptCoreLib.Shared.BCLImplementation.System.Collections;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Collections
{
    [Script(Implements = typeof(Comparer))]
    internal class __Comparer : __IComparer
    {
        static __Comparer()
        {
            Default = new __Comparer();
        }

        public static readonly __Comparer Default;



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

            var r = -2;

            if (a is int)
                if (b is int)
                {
                    var x = (int)a;
                    var y = (int)b;

                    return x.CompareTo(y);
                }

            if (a is string)
                if (b is string)
                {
                    var x = (string)a;
                    var y = (string)b;

                    return x.CompareTo(y);
                }


            //if (Expando.Of(ka).IsString)
            //    r = Expando.Compare(ka, kb);

            //if (Expando.Of(ka).IsNumber)
            //    r = Expando.Compare(ka, kb);

            //if (Expando.Of(ka).IsBoolean)
            //    r = Expando.Compare(ka, kb);


            if (r == -2)
            {
                Native.API.var_dump(a);
                Native.API.var_dump(b);

                throw new Exception("CompareTo not implemented " + new { a, b });
            }

            return r;

        }

        #endregion
    }
}
