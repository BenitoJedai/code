using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestNestedStructWithStaticFieldBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = AppDomain.CurrentDomain.DefineDynamicAssembly(
                new AssemblyName("foo"), System.Reflection.Emit.AssemblyBuilderAccess.RunAndSave
            );

            var m = a.DefineDynamicModule("foo.dll");

            var p = m.DefineType("p");
            var s = p.DefineNestedType("s");


            p.CreateType();
            s.CreateType();

            a.Save("foo.dll");
        }
    }
}
