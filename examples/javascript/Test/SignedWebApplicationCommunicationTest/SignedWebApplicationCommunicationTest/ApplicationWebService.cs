using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SignedWebApplicationCommunicationTest
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
        public void WebMethod2(string e, Action<string> y)
        {
            // Send it back to the caller.
            y(e);
        }

        public Task<signedResponse> getResp(signedResponse s)
        {
            var temp = new signedResponse();

           
                temp.resp = s.resp;

                var b = Encoding.ASCII.GetBytes(s.resp);
                var key = Encoding.ASCII.GetBytes("012345678901234567890123");
                var iv = Encoding.ASCII.GetBytes("00000000");

                TripleDES des = TripleDES.Create();
                des.Mode = CipherMode.CBC;
                des.Key = key;
                des.IV = iv;
                ICryptoTransform ic = des.CreateEncryptor();

                byte[] enc = ic.TransformFinalBlock(b, 0, b.Length);
                var i = UTF8Encoding.UTF8.GetString(enc);
                Console.WriteLine(new { i, s.signed });
                Console.WriteLine(i.Equals(s.signed));

                if (s.signed != null)
                {
                    Console.WriteLine("Inside not null");
                    if (!s.signed.Equals(i))
                    {
                        Console.WriteLine("Inside I not s.signed");

                        temp.resp = "False";
                        temp.signed = "";
                    }
                    else
                    {
                        temp.signed = i;
                    }
                }
                else
                {
                    Console.WriteLine("Inside else");
                    temp.signed = i;
                }

            return temp.AsResult();
        }

    }
    public class signedResponse
    {
        public string resp;
        public string signed;
    }
}
