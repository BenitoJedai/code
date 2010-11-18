using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace RewriteToJavaConsoleApplicationWithSerialization
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = new Tuple
            {
                Data1 = "from jvm",
                Data2 = new byte[] { 2, 3 }
            };

            var y = x.Exchange();

            // out of scope = unload?
            // never used again? :)
            // carbage collection?

            Console.WriteLine(y.Data1);

            foreach (var item in y.Data2)
            {
                Console.Write(item.ToString("x2"));
            }
            Console.WriteLine();
        }
    }

    class Tuple
    {
        public string Data1;
        public byte[] Data2;
    }

    [SwitchToCLRContext]
    static class ExtensionsToSwitchToCLRContext
    {
        public static Tuple Exchange(this Tuple data)
        {
            return new Tuple
            {
                Data1 = "from CLR - " + data.Data1,
                Data2 = new byte[] { 1, data.Data2[0] }
            };
        }
    }
}
