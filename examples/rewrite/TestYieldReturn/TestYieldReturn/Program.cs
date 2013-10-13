using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Obfuscation(Feature = "script")]

namespace TestYieldReturn
{
    class Program
    {
        ScriptCoreLib.Shared.IAssemblyReferenceToken ref0;

        static void Main(string[] args)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201310/20131013-yield
            var i = GetItems().ToArray();
        }

        public static IEnumerable<object> GetItems()
        {
            Console.WriteLine("enter GetItems");
            for (int i = 0; i < 5; i++)
            {
                yield return new object();
            }
            Console.WriteLine("exit GetItems");
        }

        //X:\jsc.svn\examples\rewrite\TestYieldReturn\TestYieldReturn\bin\Debug>TestYieldReturn.Rewrite.exe
        //<0000> ldarg.0
        //<0019> br.s
        //<001d> ldarg.0
        //enter GetItems
        //<0066> ldarg.0
        //<0039> nop
        //<0083> ldloc.0
        //<0000> ldarg.0
        //<0017> br.s
        //<0050> ldarg.0
        //<0066> ldarg.0
        //<0039> nop
        //<0083> ldloc.0
        //<0000> ldarg.0
        //<0017> br.s
        //<0050> ldarg.0
        //<0066> ldarg.0
        //<0039> nop
        //<0083> ldloc.0
        //<0000> ldarg.0
        //<0017> br.s
        //<0050> ldarg.0
        //<0066> ldarg.0
        //<0039> nop
        //<0083> ldloc.0
        //<0000> ldarg.0
        //<0017> br.s
        //<0050> ldarg.0
        //<0066> ldarg.0
        //<0039> nop
        //<0083> ldloc.0
        //<0000> ldarg.0
        //<0017> br.s
        //<0050> ldarg.0
        //<0066> ldarg.0
        //<0073> ldstr
        //exit GetItems
        //<007f> ldc.i4.0
        //<0083> ldloc.0

    }
}
