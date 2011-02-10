using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypeExtension
{
    class FooType
    {
        public string Text { get; set; }
    }

    class Program
    {
        public static void Foo(FooType f)
        {

        }

        public static void Main(string[] args)
        {
            Foo(
                new FooType { Text = "this is foo" }   
            );
        }
    }
}
