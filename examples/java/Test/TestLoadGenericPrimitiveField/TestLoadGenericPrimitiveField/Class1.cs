using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]

namespace TestLoadGenericPrimitiveField
{
    public class Class1 :
        global::ScriptCoreLibJava.IAssemblyReferenceToken
    {
        class Foo<T>
        {
            public T Current { get; set; }
        }

        //Y:\staging\web\java\ScriptCoreLib\Shared\BCLImplementation\System\Linq\__Enumerable.java:1720: error: inconvertible types
        //                num2 = ((int)(enumerator_15.System_Collections_Generic_IEnumerator_1_get_Current()));
        //                             ^
        //  required: int
        //  found:    Double
        //Y:\staging\web\java\ScriptCoreLib\Shared\BCLImplementation\System\Linq\__Enumerable.java:1769: error: inconvertible types

        public Class1()
        {
            //{
            //    var f = new Foo<int>();
            //    var i = f.Current;
            //}

            //{
            //    var f = new Foo<double>();
            //    var i = f.Current;
            //}

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131230-generic

            var z = new __item { Int32 = 0 };
            //var x = new[] { z };
            var x = new[] { z };
            var a = x.Average(xx => xx.Int32);

            Console.WriteLine(new { a });
        }

        class __item
        {
            public int Int32;
        }

    }
}
