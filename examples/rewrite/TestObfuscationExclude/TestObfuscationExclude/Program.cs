using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace TestObfuscationExclude
{
    [Obfuscation(Exclude = true)]
    public class Service
    {
        static void Foo(string[] args)
        {
            Console.WriteLine();
        }
    }

    class Internal
    {
        static void Foo(string[] args)
        {
            Console.WriteLine();
        }
    }

    // jsc does not implement this yet
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
        }
    }
}
