using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace RewriteToJavaConsoleApplicationWithByteArray
{
    class Program
    {
        static void Main(string[] args)
        {
            Transmit();
            Transmit();
        }

        private static void Transmit()
        {
            var x = new byte[] { 1, 0x7f, 0xff };

            foreach (var item in x)
            {
                Console.Write(item.ToString("x2"));
            }
            Console.WriteLine();

            var y = x.GetBytes();

            foreach (var item in y)
            {
                Console.Write(item.ToString("x2"));
            }
            Console.WriteLine();
        }
    }

    [SwitchToCLRContext]
    static class ExtensionsToSwitchToCLRContext
    {
        public static byte[] GetBytes(this byte[] data)
        {
            return new byte[] { 0x12, data[0] };
        }
    }
}
