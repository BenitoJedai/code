using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestUInt16ArrayInitializer
{
    enum X
    {
        A
    }
    class MyClass
    {
        const int Uxs = 66;
        static long Field1;
        static X x;
        static string U;
        const string C = "";

        public static void Main()
        {
            var u = new ushort[] { 0, 1, 2, 3 };
        }
    }
}
