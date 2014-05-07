using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ScriptCoreLib.Ultra.Library.Extensions;

namespace TestMD5andXORinClient
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        /// <summary>
        /// This Method is a javascript callable method.
        /// </summary>
        /// <param name="e">A parameter from javascript.</param>
        /// <param name="y">A callback to javascript.</param>
       public Task<bool> ReceiveTransact(string f1, string f2,string f3,string f4,string f5,string f6,string f7,string f8,string f9,string f10,string f11,string f12, string hash)
        {
            var timer = new Stopwatch();
            timer.Start();
            //MD5 md5 = System.Security.Cryptography.MD5.Create();

            var hashArray = new byte[12][];
           
            hashArray[0] = System.Text.Encoding.ASCII.GetBytes(f1.ToString()).ToMD5Bytes();
            hashArray[1] = System.Text.Encoding.ASCII.GetBytes(f2.ToString()).ToMD5Bytes();
            hashArray[2] = System.Text.Encoding.ASCII.GetBytes(f3.ToString()).ToMD5Bytes();
            hashArray[3] = System.Text.Encoding.ASCII.GetBytes(f4.ToString()).ToMD5Bytes();
            hashArray[4] = System.Text.Encoding.ASCII.GetBytes(f5.ToString()).ToMD5Bytes();
            hashArray[5] = System.Text.Encoding.ASCII.GetBytes(f6.ToString()).ToMD5Bytes();
            hashArray[6] = System.Text.Encoding.ASCII.GetBytes(f7.ToString()).ToMD5Bytes();
            hashArray[7] = System.Text.Encoding.ASCII.GetBytes(f8.ToString()).ToMD5Bytes();
            hashArray[8] = System.Text.Encoding.ASCII.GetBytes(f9.ToString()).ToMD5Bytes();
            hashArray[9] = System.Text.Encoding.ASCII.GetBytes(f10.ToString()).ToMD5Bytes();
            hashArray[10] = System.Text.Encoding.ASCII.GetBytes(f11.ToString()).ToMD5Bytes();
            hashArray[11] = System.Text.Encoding.ASCII.GetBytes(f12.ToString()).ToMD5Bytes();

            var finalHash = new byte[16];

            for (var f = 0; f < hashArray.Length; f++)
            {
                for (var l = 0; l < 16; l++)
                {
                    if (f == 0)
                    {
                        finalHash[l] = (byte)(hashArray[f][l] ^ hashArray[f][l + 1]);
                        f++;
                    }
                    else
                    {
                        finalHash[l] = (byte)(finalHash[l] ^ hashArray[f][l]);
                    }
                }
            }

            Console.WriteLine(timer.ElapsedMilliseconds+"ms");


            Console.WriteLine(hash);
            Console.WriteLine(finalHash.ToHexString());

            if (finalHash.ToHexString() == hash)
            {
                return true.AsResult();
            }


            return false.AsResult();
        }

    }
}
