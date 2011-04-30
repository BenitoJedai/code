using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace UnsafePointer
{
    [StructLayout(LayoutKind.Sequential)]
    struct Foo
    {
    }

    unsafe class Program
    {
        static Foo* foo;

        static void Main(string[] args)
        {
            Foo* foo0;
            Foo* foo1;

            foo0 = foo;
            foo1 = foo0;
        }
    }
}
