using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSwitchReadStringIntoBuffer
{
    class Program
    {
        private static char[] _chars = "\0hello".ToCharArray();

        private static int charPos;

        // we need the switch damnet.
        private static void ReadStringIntoBuffer()
        {
            Console.WriteLine("enter");

            // no switch opcode?
            // can we establish this works after rewrite?

            switch (_chars[charPos++])
            {
                case '\0':
                    Console.WriteLine("1");
                   
                    return;

                case '\'':
                    Console.WriteLine("2");

                    break;
            }
            Console.WriteLine("other");
        }


        static void Main(string[] args)
        {
            // X:\opensource\github\Newtonsoft.Json\Src\Newtonsoft.Json\JsonTextReader.cs

            Program .ReadStringIntoBuffer();

        }
    }
}
