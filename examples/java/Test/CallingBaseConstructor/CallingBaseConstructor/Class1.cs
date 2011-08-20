using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CallingBaseConstructor
{
    public class Class1 : MyClass
    {
        public object ClassCache = new object();
        public object ClassBytes = new object();

        public Class1(object x1, object x2)
            : base(e1: x1, e2: x2)
        {

        }
    }

    public class MyClass
    {
        public MyClass(object e1, object e2)
        {

        }
    }
}
