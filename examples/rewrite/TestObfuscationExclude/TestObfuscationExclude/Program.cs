using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace TestObfuscationExclude
{
    class Internal
    {
        static void Foo(string[] args)
        {
            Console.WriteLine();
        }
    }

    // jsc does not implement this yet
    //[Obfuscation(Exclude = true)]
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
        }
    }
}
