using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestReturnFromLoop
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Foo(args));
        }

        static string Foo(string[] args)
        {
            foreach (var item in args)
            {
                return item;
            }

            return null;
        }

        static string Bar(string[] args)
        {
            foreach (var item in args)
            {
                if (item != null)
                    return item;

                try
                {
                    Console.WriteLine();
                }
                catch
                {
                }
            }

            return null;
        }
    }
}
