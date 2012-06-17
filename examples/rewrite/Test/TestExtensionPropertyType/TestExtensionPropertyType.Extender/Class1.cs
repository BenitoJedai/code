using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestExtensionPropertyType
{

    public class Class1
    {
        //public object[] foo { get; set; }
        public Array foo { get; set; }
    }

    static class CanCast
    {
        public static void Method()
        {
            var u = default(Class1[]);
            //var o = default(object[]);
            var o = default(Array);

            o = u;
        }
    }
}
