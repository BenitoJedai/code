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
        Foo* foo;

        static void Main(string[] args)
        {
        }
    }
}
