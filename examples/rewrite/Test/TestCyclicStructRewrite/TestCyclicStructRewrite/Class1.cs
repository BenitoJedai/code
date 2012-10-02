extern alias cyclic;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestCyclicStructRewrite
{
    //class a<T>
    public struct a<T>
    {
        //T[] e;
    }

    //class c
    struct c
    {
        //a<u> a;
        cyclic::TestCyclicStructRewrite.a<u> a;
    }



    class u
    {
        c c;
    }
}
