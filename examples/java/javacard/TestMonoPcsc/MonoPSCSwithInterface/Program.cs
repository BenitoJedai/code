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
            // Send SELECT command
            // Select command 0x00, 0xA4, 0x04, 0x00,
            //Length  0x08
            //AID A0A1A2A3A4000301
            //byte[] select = new byte[] { 0x00, 0xA4, 0x04, 0x00, 0x08, 0xa0, 0xa1, 0xa2, 0xa3, 0xa4, 0x00, 0x03, 0x01 };
            //F0 45 73 74 45 49 44 20 76 65 72 20 31 2E 30

            //v.3.0 cards
            byte[] select = new byte[] { 0x00, 0xA4, 0x04, 0x00, 0x0E, 0xF0, 0x45, 0x73, 0x74, 0x45, 0x49, 0x44, 0x20, 0x76, 0x65, 0x72, 0x20, 0x31, 0x2E, 0x30 };
            //below v.3.0 vards
            byte[] select2 = new byte[] { 0x00, 0xA4, 0x04, 0x00, 0x10, 0xD2, 0x33, 0x00, 0x00, 0x01, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x27 };

            byte[] masterFile = new byte[] { 0x00, 0xA4, 0x00, 0x0C };
            byte[] EEEEcatalogue = new byte[] { 0x00, 0xA4, 0x01, 0x0C, 0x02, 0xEE, 0xEE };
            byte[] SelectFile = new byte[] { 0x00, 0xA4, 0x02, 0x04, 0x02, 0x50, 0x44, 0x00 };
            byte[] GetSurname = new byte[] { 0x00, 0xB2, 0x01, 0x04, 0x00 };
            byte[] GetFirstName = new byte[] { 0x00, 0xB2, 0x02, 0x04, 0x00 };
            byte[] Sex = new byte[] { 0x00, 0xB2, 0x03, 0x04, 0x00 };

            byte[] PinCounterFile = new byte[] { 0x00, 0xA4, 0x02, 0x0C, 0x02, 0x00, 0x16 };
            byte[] Pin1Counter = new byte[] { 0x00, 0xB2, 0x01, 0x04, 0x00};




            //var surnameLen = new byte[1];
            //byte[] GetSurname = new byte[] { 0x00, 0xC0, 0x00, 0x00, surnameLen[0] };

            {
                Console.Write("Select: ");
            var res1 = i.SendBytes(select2);
                for (var i2 = 0; i2 < res1.Length; i2++)
                    Console.Write("{0:X2} ", res1[i2]);
                Console.WriteLine();
            }
            {
                Console.Write("MasterFile: ");
                var res2 = i.SendBytes(masterFile);
                for (var i2 = 0; i2 < res2.Length; i2++)
                    Console.Write("{0:X2} ", res2[i2]);
                Console.WriteLine();
            }
            {
                Console.Write("EEEE catalogue: ");
                var res2 = i.SendBytes(EEEEcatalogue);
                for (var i2 = 0; i2 < res2.Length; i2++)
                    Console.Write("{0:X2} ", res2[i2]);
                Console.WriteLine();
            }
            {
                Console.Write("SelectFile: ");
                var res2 = i.SendBytes(SelectFile);
                for (var i2 = 0; i2 < res2.Length; i2++)
                    Console.Write("{0:X2} ", res2[i2]);
                Console.WriteLine();
                Task.Delay(10000);
            }
            //{
            //    //below JavaCard v.3.0
            //    Console.Write("Surname: ");
            //    var res2 = i.SendBytes(GetSurname);
            //    //surnameLen[0] = res2[1];
            //    for (var i2 = 0; i2 < res2.Length; i2++)
            //        Console.Write("{0:X2} ", res2[i2]);
            //    Console.Write(Encoding.ASCII.GetString(res2));
            //    Console.WriteLine();
            //}
            ////Javacard v.3.0
            ////{
            ////    Console.Write("Surname: ");
            ////    var res2 = i.SendBytes(GetSurname);
            ////    for (var i2 = 0; i2 < res2.Length; i2++)
            ////        Console.Write("{0:X2} ", res2[i2]);
            ////    Console.WriteLine();
            ////}

            //{
            //    //below JavaCard v.3.0
            //    Console.Write("First name: ");
            //    var res2 = i.SendBytes(GetFirstName);
            //    //surnameLen[0] = res2[1];
            //    for (var i2 = 0; i2 < res2.Length; i2++)
            //        Console.Write("{0:X2} ", res2[i2]);
            //    Console.Write(Encoding.ASCII.GetString(res2));
            //    Console.WriteLine();
            //}
            //{
            //    //below JavaCard v.3.0
            //    Console.Write("First name: ");
            //    var res2 = i.SendBytes(GetFirstName);
            //    //surnameLen[0] = res2[1];
            //    for (var i2 = 0; i2 < res2.Length; i2++)
            //        Console.Write("{0:X2} ", res2[i2]);
            //    Console.Write(Encoding.ASCII.GetString(res2));
            //    Console.WriteLine();
            //}
            byte b = (byte)0x01;

            for (int c = 1; c <= 16; c++)
            {
                byte[] temp = new byte[] { 0x00, 0xB2, b, 0x04, 0x00 };
                var res2 = i.SendBytes(temp);
                var n = new byte[res2.Length];
                Array.Copy(res2,n,res2.Length - 2);
                for (var i2 = 0; i2 < n.Length; i2++)
                    Console.Write("{0:X2} ", n[i2]);
                Console.WriteLine(Encoding.ASCII.GetString(n));
                b++;
            }



            //{
            //    Console.Write("MasterFile: ");
            //    var res2 = i.SendBytes(masterFile);
            //    for (var i2 = 0; i2 < res2.Length; i2++)
            //        Console.Write("{0:X2} ", res2[i2]);
            //    Console.WriteLine();
            //}
            //{
            //    Console.Write("Select pin counter file: ");
            //    var res2 = i.SendBytes(PinCounterFile);
            //    for (var i2 = 0; i2 < res2.Length; i2++)
            //        Console.Write("{0:X2} ", res2[i2]);
            //    Console.WriteLine();
            //    Task.Delay(10000);
            //}
            //{
            //    Console.Write("Pin1 counter: ");
            //    var res2 = i.SendBytes(Pin1Counter);
            //    for (var i2 = 0; i2 < res2.Length; i2++)
            //        Console.Write("{0:X2} ", res2[i2]);
            //    Console.WriteLine();
            //    Task.Delay(10000);
            //}

            Console.ReadLine();
        }
    }
}
