extern alias cyclic;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    public struct a<T>
    {
    }

    struct c
    {
        //a<u> a;
        a<cyclic::u> a;
    }



    class u
    {
        c c;
    }
