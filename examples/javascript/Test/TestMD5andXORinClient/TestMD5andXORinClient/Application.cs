using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestMD5andXORinClient;
using TestMD5andXORinClient.Design;
using TestMD5andXORinClient.HTML.Pages;
using ScriptCoreLib.Ultra.Library.Extensions;

namespace TestMD5andXORinClient
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            var button = new IHTMLButton { innerText = "CalculateMD5Hash"}.AttachToDocument();

            button.onclick += async delegate
            {
                var timer = new Stopwatch();
                timer.Start();
                //MD5 md5 = System.Security.Cryptography.MD5.Create();

                var hashArray = new byte[12][];
                var f1 = "1";
                var f2 = "Hello";
                var f3 = "My";
                var f4 = "Brother";
                var f5 = "From another mother";
                var f6 = "How";
                var f7 = "j4wpofjw4pjf4w";
                var f8 = "The hell";
                var f9 = "ewfwf3ff";
                var f10 = "HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH";
                var f11 = "4jfo4wjeojerpogjeoprjgrpe";
                var f12 = "efoiwhrghowhgoi";



                //hashArray[0] = md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes("1".ToString()));
                //hashArray[1] = md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes("Hello".ToString()));
                //hashArray[2] = md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes("My".ToString()));
                //hashArray[3] = md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes("Friend".ToString()));
                //hashArray[4] = md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes("This is test".ToString()));
                //hashArray[5] = md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes("of".ToString()));
                //hashArray[6] = md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes("MD5 hash".ToString()));
                //hashArray[7] = md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes("And".ToString()));
                //hashArray[8] = md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes("XOS".ToString()));
                //hashArray[9] = md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes("array".ToString()));
                //hashArray[10] = md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes("merge".ToString()));
                //hashArray[11] = md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes("to get transaction hash".ToString()));

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

               

                Console.WriteLine(finalHash.ToHexString() + " "+timer.ElapsedMilliseconds+"ms");

                var result = await this.ReceiveTransact(f1,f2,f3,f4,f5,f6,f7,f8,f9,f10,f11,f12,finalHash.ToHexString());
                Console.WriteLine(result.ToString());


            };


           

        }

    }
}
