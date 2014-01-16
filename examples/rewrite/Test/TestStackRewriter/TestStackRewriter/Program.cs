using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestStackRewriter
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140115/xlsx

            var a = AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName("foo"), System.Reflection.Emit.AssemblyBuilderAccess.Save);
            var e = new { Module = a.DefineDynamicModule("foo") };


            var xExtensions = e.Module.DefineType("Extensions");
            var xRow = e.Module.DefineType("Row");
            Console.WriteLine("define xExtensionsAsDataTable");

            Test(xExtensions, xRow);

        }

        private static void Test(System.Reflection.Emit.TypeBuilder xExtensions, System.Reflection.Emit.TypeBuilder xRow)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140115/xlsx
            xExtensions.DefineMethod("AsDataTable",
                default(MethodAttributes),
                null,
                // parameter types
                // there is a fault here.
                new[] { typeof(IEnumerable<>).MakeGenericType(xRow) }
            );
        }


        //static void XMain(string[] args)
        //{
        //    var xx =
        //        from x in new { a = 1, b = 2, c = 3, e = new[] { new { z = 0 } } }

        //        where x.a == 1
        //        where x.b == 2
        //        where x.c == 3

        //        select x;

        //}
    }

    //public static class X
    //{
    //    public static T Where<T>(this T that, params Expression<Func<T, bool>>[] f)
    //    {
    //        Console.WriteLine("Where");

    //        return that;
    //    }
    //}
}
