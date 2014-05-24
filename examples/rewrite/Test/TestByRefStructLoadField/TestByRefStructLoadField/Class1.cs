using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestByRefStructLoadField
{
    public struct __Invoke
    {
        public int __state;
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140524

        public class __MoveNext
        {
            public static int _000_try_(__MoveNext that, ref __Invoke arg1)
            {
                int loc1 = arg1.__state;



                foo.copy(arg1);

                foo.byref(ref arg1);

                return 0;
            }
        }



    }

    static class foo
    {
        public static void copy(__Invoke e)
        {

        }

        public static void byref(ref __Invoke e)
        {

        }
    }

}
