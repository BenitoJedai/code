using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]
namespace TestGenericField
{
    static class Foo<T>
    {
        public static object Text;
    }


    public class Class1
    {
        // X:\jsc.svn\examples\java\hybrid\Test\TestJVMCLRGenericField\TestJVMCLRGenericField\Program.cs
        public Class1()
        {
            Foo<object>.Text = "1";

            var x = Foo<object>.Text;

            //Foo<Class1>.Text = "2";
        }
    }
}
