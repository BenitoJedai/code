using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace ConsoleApplication1
{
    class Foo
    {
        public static void Bar()
        {
            int caseSwitch = 1;
            switch (caseSwitch)
            {
                case 1:
                    Console.WriteLine("Case 1");
                    break;
                case 2:
                    Console.WriteLine("Case 2");
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }

        }
        /*
         
.method public hidebysig static void Bar() cil managed
{
    .maxstack 2
    .locals init (
        [0] int32 caseSwitch,
        [1] int32 CS$4$0000)
    L_0000: nop 
    L_0001: ldc.i4.1 
    L_0002: stloc.0 
    L_0003: ldloc.0 
    L_0004: stloc.1 
    L_0005: ldloc.1 
    L_0006: ldc.i4.1 
    L_0007: sub 
    L_0008: switch (L_0017, L_0024)
    L_0015: br.s L_0031
    L_0017: ldstr "Case 1"
    L_001c: call void [mscorlib]System.Console::WriteLine(string)
    L_0021: nop 
    L_0022: br.s L_003e
    L_0024: ldstr "Case 2"
    L_0029: call void [mscorlib]System.Console::WriteLine(string)
    L_002e: nop 
    L_002f: br.s L_003e
    L_0031: ldstr "Default case"
    L_0036: call void [mscorlib]System.Console::WriteLine(string)
    L_003b: nop 
    L_003c: br.s L_003e
    L_003e: ret 
}

 

 
         
         */

        public static string Bar(int caseSwitch2)
        {
            int caseSwitch = 1;
            switch (caseSwitch)
            {
                case 1:
                    Console.WriteLine("Case 1");
                    break;
                case 2:
                    Console.WriteLine("Case 2");
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }

            switch (caseSwitch2)
            {
                case 1:
                    return ("Case 1");
                case 2:
                    return ("Case 2");
                default:
                    return ("Default case");
            }
        }

        public  static void Workflow(object e)
        {
            var offset = 0;

            while (offset >= 0)
            {
                if (offset == 0)
                    offset = Flow0(e);

                //if (offset == 2)
                //    offset = Flow2();
            }
        }

        private static int Flow0(object e)
        {
            return -1;
        }

        //private int Flow2()
        //{
        //    return -1;
        //}

    }

    class Program
    {
        /*
         
00:00:00.4971035
00:00:00.5070391
00:00:00.5139857
00:00:00.6046821
00:00:00.4986795
00:00:00.5341889
00:00:00.5070929
00:00:00.5171428
00:00:00.5901643
00:00:00.7211178
         * 
         */

        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                Measure1();

            }
        }

        private static void Measure1()
        {
            var o = Console.Out;
            Console.SetOut(new StringWriter());

            var x = Stopwatch.StartNew();
            for (int i = 0; i < 0x100000; i++)
            {
                Foo.Bar();
            }
            x.Stop();

            Console.SetOut(o);
            Console.WriteLine(x.Elapsed);
        }
    }
}
