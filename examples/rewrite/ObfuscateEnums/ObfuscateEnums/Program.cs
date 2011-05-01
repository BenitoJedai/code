using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObfuscateEnums
{
    enum Foo
    {
        Hello,
        World
    }

    class Program
    {
        static void Main(string[] args)
        {
            var foo = Foo.Hello;

            Console.WriteLine(foo + "");
        }
    }
}
