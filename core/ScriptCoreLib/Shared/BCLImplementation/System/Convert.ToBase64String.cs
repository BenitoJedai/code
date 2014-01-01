using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace ScriptCoreLib.Shared.BCLImplementation.System
{
    internal static partial class __Convert
    {
        #region Base64Key
        internal const string Base64Key = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";




        public static string ToBase64String(byte[] input)
        {
            Console.WriteLine("enter ToBase64String");
            var ToBase64String_while_timeout = Stopwatch.StartNew();

            var capacity = 3 * input.Length / 4;



            var w = new StringBuilder
            {
                Capacity = capacity
            };

            int chr1, chr2, chr3;
            int enc1, enc2, enc3, enc4;
            int i = 0;
            var length = input.Length;

            bool b = i < length;

            while (b)
            {
                enc3 = 64;
                enc4 = 64;

                chr1 = input[i++];
                enc1 = chr1 >> 2;
                enc2 = ((chr1 & 3) << 4);

                if (i < length)
                {
                    chr2 = input[i++];
                    enc2 |= (chr2 >> 4);
                    enc3 = ((chr2 & 15) << 2);
                }

                if (i < length)
                {
                    chr3 = input[i++];
                    enc3 |= (chr3 >> 6);
                    enc4 = chr3 & 63;
                }

                w.Append(Base64Key[enc1]);
                w.Append(Base64Key[enc2]);
                w.Append(Base64Key[enc3]);
                w.Append(Base64Key[enc4]);



                b = i < length;


                if (ToBase64String_while_timeout.ElapsedMilliseconds > 500)
                {
                    Console.WriteLine(new { ToBase64String_while_timeout, i, length });
                    ToBase64String_while_timeout.Restart();
                }
            }

            return w.ToString();
        }


        #endregion


    }

}
