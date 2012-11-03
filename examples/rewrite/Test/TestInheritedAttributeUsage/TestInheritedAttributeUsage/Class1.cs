using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestInheritedAttributeUsage
{
    public class Class1
    {
        [System.Runtime.CompilerServices.AsyncStateMachineAttribute(typeof(Class1))]
        void foo()
        { }
    }
}
