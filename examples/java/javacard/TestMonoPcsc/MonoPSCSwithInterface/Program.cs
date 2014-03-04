using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMonoPcsc;

namespace MonoPSCSwithInterface
{
    class Program
    {
        static void Main(string[] args)
        {
            IMyPCSC i = new MyPCSCImplementation();
            byte[] select = new byte[] { 0x00, 0xA4, 0x04, 0x00, 0x08, 0xa0, 0xa1, 0xa2, 0xa3, 0xa4, 0x00, 0x03, 0x01 };
            byte[] test = new byte[] { 0x00, 0x00, 0x00, 0x00 };


            var res1 = i.SendBytes(select);

            for (var i2 = 0; i2 < res1.Length; i2++)
                Console.Write("{0:X2} ", res1[i2]);
            Console.WriteLine();

            var res2 = i.SendBytes(test);
            for (var i2 = 0; i2 < res2.Length; i2++)
                Console.Write("{0:X2} ", res2[i2]);

            Console.ReadLine();
        }
    }
}
