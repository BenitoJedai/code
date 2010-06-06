using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary2
{
    public class Class1
    {
        public static object Get()
        {
            return new { Field1 = default(string) };
        }
    }
}
