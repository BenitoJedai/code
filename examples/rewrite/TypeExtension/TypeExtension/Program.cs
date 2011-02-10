using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypeExtension
{
    public class BarType
    {
        public string Text { get; set; }
    }

    public class FooType
    {
        public string Text;
    }

    class Program
    {
        public static void Foo(FooType f)
        {
            Console.WriteLine(f.ToString());
        }

        public static void Main(string[] args)
        {
            Foo(
                new FooType { Text = "this is foo" }   
            );
        }
    }
}
