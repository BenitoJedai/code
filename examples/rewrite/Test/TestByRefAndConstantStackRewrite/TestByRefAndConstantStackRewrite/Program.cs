using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace TestByRefAndConstantStackRewrite
{
    struct foo
    {
        public Type x;

        public MethodBuilder m;

    }

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


            var f = new foo { x = xRow };
            Test(xExtensions, ref f);
        }

        public MethodBuilder m;


        private void Test2(System.Reflection.Emit.TypeBuilder xExtensions, ref foo f)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140118
            // X:\jsc.svn\examples\rewrite\Test\TestStackRewriter\TestStackRewriter\Program.cs

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140115/xlsx
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140118/testasyncstackrewriter

            this.m = xExtensions.DefineMethod("AsDataTable",
                default(MethodAttributes),
                null,
                // parameter types
                // there is a fault here.
                new[] { typeof(IEnumerable<>).MakeGenericType(f.x) }
            );
        }

        private static void Test(System.Reflection.Emit.TypeBuilder xExtensions, ref foo f)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140118
            // X:\jsc.svn\examples\rewrite\Test\TestStackRewriter\TestStackRewriter\Program.cs

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140115/xlsx
            f.m = xExtensions.DefineMethod("AsDataTable",
                default(MethodAttributes),
                null,
                // parameter types
                // there is a fault here.
                new[] { typeof(IEnumerable<>).MakeGenericType(f.x) }
            );
        }
    }
}
