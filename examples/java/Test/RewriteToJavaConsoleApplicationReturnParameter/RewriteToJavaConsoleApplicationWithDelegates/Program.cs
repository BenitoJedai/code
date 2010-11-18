using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using System.Threading;

namespace RewriteToJavaConsoleApplicationWithDelegates
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("begin Generate");

            TupleAction AtEvent1 =
                n =>
                {
                    Console.WriteLine("event: " + n.Data1);
                    Console.WriteLine("3");
                    Thread.Sleep(500);
                    Console.WriteLine("2");
                    Thread.Sleep(500);
                    Console.WriteLine("1");
                    Thread.Sleep(500);
                };

            ExtensionsToSwitchToCLRContext.Generate(
                AtEvent1
            );

            Console.WriteLine("end Generate");

        }
    }

    class Tuple
    {
        public string Data1;
    }

    delegate void TupleAction(Tuple e);

    [SwitchToCLRContext]
    static class ExtensionsToSwitchToCLRContext
    {
        public static void Generate(
            TupleAction AtEvent1
        )
        {
            StringAction Notify =
                text =>
                {
                    Console.WriteLine(text);

                    var a = new Tuple { Data1 = text };

                    if (AtEvent1 != null)
                        AtEvent1(a);
                };

            Notify("  beginning");

            for (int i = 0; i < 8; i++)
            {
                Thread.Sleep(500);
                Console.Write(".");
            }
            Console.WriteLine();

            Notify("  middle");

            for (int i = 0; i < 8; i++)
            {
                Thread.Sleep(500);
                Console.Write(".");
            }
            Console.WriteLine();

            Notify("  end");

        }
    }

}
