extern alias source;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypeExtension
{
    class Program
    {
        /*
         * Steps taken to implement this:
         * 01. Make jsc rewrite only TypeExtensionToFoo and verify jsc rewrite works
         * 02. Commit to svn before we break it again.
         */
        // this method shall update the existing implementation
        public static void Foo(global::TypeExtension.FooType f)
        {
            Console.WriteLine(f.FooText);
        }
    }

    public class FooType
    {
        public source::TypeExtension.BarType Bar { get; set; }

        // this member shall be resolved as already existing member
        public string Text { get; set; }

        // this member shall be added
        public string FooText { get { return this.Text + " foo"; } }
    }

}
