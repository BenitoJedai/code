using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestExtensionConstructor
{
    public class Class1
    {
        readonly Class1 bar = new Class1();

        public Class1()
        {
            Console.WriteLine("bar");
        }
    }
}
