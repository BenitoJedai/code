using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestMethodConstraintToExtendingType
{
    public class Class1
    {
        public static void Foo<T>() where T : Class2
        {

        }
    }

    public class Class2 : Class1
    {
    }

}
