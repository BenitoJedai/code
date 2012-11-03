using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestBranchWithLoadedStack
{
    public class Class1
    {
        public int int0;
        public bool bool1;

        void foo(object o0, Class1 c0, IEnumerable<Class1> source, Func<Class1, bool> f, object o, Class1 c)
        {
            switch (int0)
            {
                case 1:

                    foo(null, null, null, null, null, null);
                    break;
                case 0:
                    return;
                case 3:
                    foo(null, null, null, k => k.bool1, null, null);
                    break;
            }
        }


    }
}
