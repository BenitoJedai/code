using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypeExtension
{
    class FooType
    {
        // this member shall be resolved as already existing member
        public string Text { get; set; }

        // this member shall be added
        public string FooText { get { return this.Text + " foo"; } }
    }

    class Program
    {
        // this method shall update the existing implementation
        public static void Foo(FooType f)
        {
            Console.WriteLine(f.FooText);
        }
    }
}
